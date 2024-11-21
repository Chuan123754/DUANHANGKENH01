using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.Text.Json.Serialization;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _connectionString;

        public UploadController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        // Sheet1: Sizes
                        var sizes = ReadSheet<Size>(package, "Sizes");
                        // Sheet2: Textile_Technologies
                        var textileTechnologies = ReadSheet<Textile_technology>(package, "Textile_Technologies");
                        // Sheet3: Styles
                        var styles = ReadSheet<Style>(package, "Styles");
                        // Sheet4: Materials
                        var materials = ReadSheet<Material>(package, "Materials");
                        // Sheet5: Colors
                        var colors = ReadSheet<Color>(package, "Colors");

                        // Lưu vào database
                        using (var connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();

                            await InsertIntoDatabase(connection, sizes, "Sizes");
                            await InsertIntoDatabase(connection, textileTechnologies, "Textile_Technologies");
                            await InsertIntoDatabase(connection, styles, "Styles");
                            await InsertIntoDatabase(connection, materials, "Materials");
                            await InsertIntoDatabase(connection, colors, "Color");
                        }
                    }
                }

                return Ok("Nhập dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xử lý file: {ex.Message}");
            }
        }

        private List<T> ReadSheet<T>(ExcelPackage package, string sheetName) where T : new()
        {
            var worksheet = package.Workbook.Worksheets[sheetName];
            if (worksheet == null)
            {
                throw new Exception($"Sheet '{sheetName}' không tồn tại.");
            }

            var list = new List<T>();
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Bỏ qua header
            {
                var obj = new T();
                var props = typeof(T).GetProperties()
                    .Where(p => !p.PropertyType.Name.Contains("ICollection") && !p.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any());

                foreach (var prop in props)
                {
                    var column = worksheet.Cells[row, props.ToList().IndexOf(prop) + 1].Value;

                    if (column != null)
                    {
                        try
                        {
                            if (prop.PropertyType == typeof(DateTime))
                            {
                                // Xử lý chuyển đổi ngày/tháng
                                if (DateTime.TryParse(column.ToString(), out var dateValue))
                                {
                                    prop.SetValue(obj, dateValue);
                                }
                                else
                                {
                                    prop.SetValue(obj, DateTime.MinValue); 
                                }
                            }
                            else if (prop.PropertyType == typeof(bool?))
                            {
                                prop.SetValue(obj, bool.TryParse(column.ToString(), out var boolValue) ? boolValue : (bool?)null);
                            }
                            else if (prop.PropertyType == typeof(long) || prop.PropertyType == typeof(int))
                            {
                                prop.SetValue(obj, Convert.ToInt64(column));
                            }
                            else
                            {
                                prop.SetValue(obj, column.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Lỗi khi chuyển đổi dữ liệu cột '{prop.Name}' tại dòng {row}: {ex.Message}");
                        }
                    }
                }

                list.Add(obj);
            }

            return list;
        }



        private async Task InsertIntoDatabase<T>(SqlConnection connection, List<T> data, string tableName)
        {
            foreach (var item in data)
            {
                var query = GenerateInsertQuery(item, tableName);
                using (var command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private string GenerateInsertQuery<T>(T item, string tableName)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && !p.PropertyType.Name.Contains("ICollection") && !p.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any());

            var columns = string.Join(", ", props.Select(p => p.Name));
            var values = string.Join(", ", props.Select(p =>
            {
                var value = p.GetValue(item);
                if (value is DateTime dateTime)
                {
                    return $"'{dateTime:yyyy-MM-dd HH:mm:ss}'"; // Chuyển đổi DateTime sang định dạng SQL
                }
                else if (value is string strValue)
                {
                    return $"N'{strValue.Replace("'", "''")}'"; // Thêm N trước chuỗi Unicode
                }
                return $"'{value ?? DBNull.Value}'";
            }));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    // Sheet1: Sizes
                    var sizes = await GetDataFromDatabase("Sizes");
                    AddSheet(package, "Sizes", sizes);

                    // Sheet2: Textile_Technologies
                    var textileTechnologies = await GetDataFromDatabase("Textile_Technologies");
                    AddSheet(package, "Textile_Technologies", textileTechnologies);

                    // Sheet3: Styles
                    var styles = await GetDataFromDatabase("Styles");
                    AddSheet(package, "Styles", styles);

                    // Sheet4: Materials
                    var materials = await GetDataFromDatabase("Materials");
                    AddSheet(package, "Materials", materials);

                    // Sheet5: Colors
                    var colors = await GetDataFromDatabase("Color");
                    AddSheet(package, "Colors", colors);

                    // Trả về file Excel
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    var fileName = $"ExportedData_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xuất dữ liệu: {ex.Message}");
            }
        }

        private async Task<List<Dictionary<string, object>>> GetDataFromDatabase(string tableName)
        {
            var result = new List<Dictionary<string, object>>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT * FROM {tableName}";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        private void AddSheet(ExcelPackage package, string sheetName, List<Dictionary<string, object>> data)
        {
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            if (data.Count > 0)
            {
                // Thêm tiêu đề cột
                var headers = data[0].Keys.ToList();
                for (int col = 0; col < headers.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = headers[col];
                }

                // Thêm dữ liệu
                for (int row = 0; row < data.Count; row++)
                {
                    var rowData = data[row];
                    for (int col = 0; col < headers.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = rowData[headers[col]];
                    }
                }
            }
        }

    }
}
