using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ViewsFE.IServices;
using static ViewsFE.Pages.Admin.Size.Index;

namespace ViewsFE.Services
{
    public class UploadService : IUploadService
    {
        private readonly HttpClient _httpClient;

        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UploadResponse> UploadExcel(string tableName, IFormFile file)
        {
            using var formData = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            formData.Add(fileContent, "file", file.FileName);
            var response = await _httpClient.PostAsync($"https://localhost:7011/api/Upload/upload/{tableName}", formData);

            // Kiểm tra mã trạng thái phản hồi
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<UploadResponse>(errorContent);

                // Nếu không thể chuyển đổi, trả về lỗi mặc định
                if (errorResponse == null)
                {
                    throw new HttpRequestException($"Upload failed with status {response.StatusCode}: {response.ReasonPhrase}");
                }
                return errorResponse;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UploadResponse>(responseContent);
        }



        public async Task<byte[]> ExportExcel(string tableName)
        {
            // Export file Excel
            string requestURL = $"https://localhost:7011/api/Upload/export/{tableName}";

            var response = await _httpClient.GetAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to export file. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
