using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;
using static ViewsFE.Services.PostServices;

namespace ViewsFE.Services
{
    public class DesignerServices : IDesignerServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public DesignerServices(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<bool> CheckSlug(string slug)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Designer/checkslug?slug={slug}");
            response.EnsureSuccessStatusCode();
            return bool.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> CheckSlugForUpdate(string slug, long desiId)
        { 
            try
            {
                // Gửi request đến API
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_baseUrl}/api/Designer/check-slug-for-update?slug={slug}&desiId={desiId}");

                // Nếu response hợp lệ, trả về giá trị `IsUnique`
                return response?.IsUnique ?? false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling CheckSlugForUpdate API: {ex.Message}");
                return false; // Trả về false trong trường hợp lỗi
            }
        }

        public async Task<long> Create(Designer at)
        {
            string requestURL = $"{_baseUrl}/api/Designer";
            var response = await _httpClient.PostAsJsonAsync(requestURL, at);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
        }

        public async Task Delete(long id)
        {
            string requestURL = $"{_baseUrl}/api/Designer/{id}";
            await _httpClient.DeleteAsync(requestURL);
        }

        public async Task<List<Designer>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/Designer";
            return await _httpClient.GetFromJsonAsync<List<Designer>>(requestURL);
        }

        public async Task<List<Designer>> GetAllAC()
        {
            string requestURL = $"{_baseUrl}/api/Designer/GetAllAC";
            return await _httpClient.GetFromJsonAsync<List<Designer>>(requestURL);
        }

        public async Task<Designer> GetById(long id)
        {
           return await _httpClient.GetFromJsonAsync<Designer>($"{_baseUrl}/api/Designer/{id}");
        }

        public async Task<Designer> GetByIdSlug(string slug)
        {
            return await _httpClient.GetFromJsonAsync<Designer>($"{_baseUrl}/api/Designer/GetByIdSlug?slug={slug}");
        }

        public async Task<List<Designer>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Designer/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _httpClient.GetFromJsonAsync<List<Designer>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Designer/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

        public async Task<long> Update(Designer at)
        {
            string requestURL = $"{_baseUrl}/api/Designer/{at.id_Designer}";
            var response = await _httpClient.PutAsJsonAsync(requestURL, at);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
        }
    }
}
