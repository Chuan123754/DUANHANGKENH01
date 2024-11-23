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

        [HttpPost("upload/{tableName}")]
        public async Task<IActionResult> UploadExcel(string tableName, IFormFile file)
        {
            
            // Kiểm tra xem file có được truyền hay không
            if (file == null || file.Length == 0)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "File không hợp lệ. Vui lòng kiểm tra lại file bạn đã chọn."
                });
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var data = ReadSheet(package, tableName);

                        var successfulTitles = new List<string>();
                        var failedTitles = new List<string>();

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();

                            foreach (var item in data)
                            {
                                var title = item.ContainsKey("Title") ? item["Title"]?.ToString() : null;
                                if (string.IsNullOrEmpty(title))
                                {
                                    failedTitles.Add("(Không có tiêu đề)");
                                    continue;
                                }

                                // Kiểm tra trùng lặp
                                var checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE Title = @Title";
                                using (var checkCommand = new SqlCommand(checkQuery, connection))
                                {
                                    checkCommand.Parameters.AddWithValue("@Title", title);
                                    var count = (int)await checkCommand.ExecuteScalarAsync();

                                    if (count > 0)
                                    {
                                        failedTitles.Add(title);
                                        continue;
                                    }
                                }

                                // Nếu không trùng lặp, thêm dữ liệu
                                try
                                {
                                    await InsertIntoDatabase(connection, new List<Dictionary<string, object>> { item }, tableName);
                                    successfulTitles.Add(title);
                                }
                                catch
                                {
                                    failedTitles.Add(title);
                                }
                            }
                        }

                        return Ok(new
                        {
                            Success = true,
                            Message = $"Nhập dữ liệu vào bảng {tableName} thành công!",
                            SuccessfulTitles = successfulTitles,
                            FailedTitles = failedTitles
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = $"Lỗi khi xử lý file: {ex.Message}"
                });
            }
        }





        private List<Dictionary<string, object>> ReadSheet(ExcelPackage package, string sheetName)
        {
            var worksheet = package.Workbook.Worksheets[sheetName];
            if (worksheet == null)
            {
                throw new Exception($"Sheet '{sheetName}' không tồn tại.");
            }

            var data = new List<Dictionary<string, object>>();
            int rowCount = worksheet.Dimension.Rows; // Số dòng
            int colCount = worksheet.Dimension.Columns; // Số cột

            // Đọc tiêu đề cột từ hàng đầu tiên
            var headers = new List<string>();
            for (int col = 1; col <= colCount; col++)
            {
                headers.Add(worksheet.Cells[1, col].Value?.ToString()?.Trim());
            }

            // Đọc dữ liệu từ các dòng tiếp theo
            for (int row = 2; row <= rowCount; row++) // Bỏ qua hàng đầu tiên (header)
            {
                var rowData = new Dictionary<string, object>();
                for (int col = 1; col <= colCount; col++)
                {
                    var header = headers[col - 1]; // Lấy tiêu đề cột
                    var value = worksheet.Cells[row, col].Value; // Lấy giá trị tại ô hiện tại
                    rowData[header] = value ?? DBNull.Value; // Nếu giá trị null, gán DBNull.Value
                }
                data.Add(rowData);
            }

            return data;
        }


        private async Task InsertIntoDatabase(SqlConnection connection, List<Dictionary<string, object>> data, string tableName)
        {
            foreach (var item in data)
            {
                // Bỏ qua cột Id
                var filteredItem = item.Where(kv => kv.Key.ToLower() != "id").ToDictionary(kv => kv.Key, kv => kv.Value);

                // Lấy giá trị cột 'Title' để kiểm tra trùng lặp
                if (!filteredItem.ContainsKey("Title"))
                {
                    throw new Exception("Dữ liệu không có cột 'Title', không thể kiểm tra trùng lặp.");
                }

                var title = filteredItem["Title"].ToString();

                // Kiểm tra trùng lặp
                var checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE Title = @Title";
                using (var checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Title", title);

                    var count = (int)await checkCommand.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        Console.WriteLine($"Bỏ qua bản ghi trùng lặp: {title}");
                        continue; 
                    }
                }

                // Nếu không trùng lặp, thêm dữ liệu vào bảng
                var columns = string.Join(", ", filteredItem.Keys);
                var values = string.Join(", ", filteredItem.Values.Select(value =>
                {
                    if (value is DateTime dateTime)
                    {
                        return dateTime == DateTime.MinValue
                            ? "'0001-01-01 00:00:00.0000000'"
                            : $"'{dateTime:yyyy-MM-dd HH:mm:ss.fffffff}'";
                    }
                    else if (value is string strValue)
                    {
                        return $"N'{strValue.Replace("'", "''")}'";
                    }
                    else if (value is bool boolValue)
                    {
                        return boolValue ? "1" : "0"; // Chuyển đổi bool thành 1/0
                    }
                    else if (value == null || value == DBNull.Value)
                    {
                        return "NULL";
                    }
                    return value.ToString();
                }));

                var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                Console.WriteLine($"Generated Query: {query}"); // Log query để debug

                using (var command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }



        [HttpGet("export/{tableName}")]
        public async Task<IActionResult> ExportExcel(string tableName)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var data = await GetDataFromDatabase(tableName);
                    AddSheet(package, tableName, data);

                    // Trả về file Excel
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    var fileName = $"ExportedData_{tableName}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
