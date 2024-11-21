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

        public async Task<Designer> GetById(long id)
        {
      //  https://localhost:7011/api/Designer/2
            string requestURL = $"{_baseUrl}/api/Designer/{id}";
            return await _httpClient.GetFromJsonAsync<Designer>(requestURL);
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
