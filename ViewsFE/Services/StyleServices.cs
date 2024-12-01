using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class StyleServices : IStyleServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public StyleServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Style s)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Style", s);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Style/{id}");
        }

        public async Task<Style> Details(long id)
        {
            return await _client.GetFromJsonAsync<Style>($"{_baseUrl}/api/Style/{id}");
        }

        public async Task<List<Style>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Style>>($"{_baseUrl}/api/Style");
        }

        public async Task<List<Style>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Style/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Style>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Style/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }
        public async Task Update(Style s)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Style/update-style", s);
        }
    }
}
