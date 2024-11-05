using Newtonsoft.Json;
using ClientViews.IServices;
using ClientViews.Models;

namespace ClientViews.Services
{
    public class Textile_technologyServices : ITextile_technologyServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public Textile_technologyServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Textile_technology t)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Textile_technology", t);
        }
        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Textile_technology/{id}");
        }
        public async Task<Textile_technology> Details(long id)
        {
            return await _client.GetFromJsonAsync<Textile_technology>($"{_baseUrl}/api/Textile_technology/{id}");
        }
        public async Task<List<Textile_technology>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Textile_technology>>($"{_baseUrl}/api/Textile_technology");
        }

        public async Task<List<Textile_technology>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Textile_technology/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Textile_technology>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Textile_technology/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }
        public async Task Update(Textile_technology t)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Textile_technology/{t.Id}", t);
        }
    }
}
