using Newtonsoft.Json;
using ClientViews.IServices;
using ClientViews.Models;

namespace ClientViews.Services
{
    public class MaterialServices : IMaterialServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public MaterialServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Material s)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Material", s);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Material/{id}");
        }

        public async Task<Material> Details(long id)
        {
            return await _client.GetFromJsonAsync<Material>($"{_baseUrl}/api/Material/{id}");
        }

        public async Task<List<Material>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Material>>($"{_baseUrl}/api/Material");
        }

        public async Task<List<Material>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {

            var uri = $"{_baseUrl}/api/Material/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Material>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Material/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

      
        public async Task Update(Material s)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Material/{s.Id}", s);
        }
    }
}
