using Newtonsoft.Json;
using System.Drawing.Printing;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class SizeServices : ISizeServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public SizeServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Size s)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Size", s);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Size/{id}");
        }

        public async Task<Size> Details(long id)
        {
            return await _client.GetFromJsonAsync<Size>($"{_baseUrl}/api/Size/{id}");
        }

        public async Task<List<Size>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Size>>($"{_baseUrl}/api/Size");
        }

        public async Task<List<Size>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Size/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Size>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Size/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

       

        public async Task Update(Size s)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Size/{s.Id}", s);
        }
    }
}
