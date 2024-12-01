using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ContacServices : IContacServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ContacServices(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Contact c)
        {
            await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Contact/CreateContact", c);
        }

        public async Task Delete(long id)
        {   
            await _httpClient.DeleteAsync($"{_baseUrl}/api/Contact/DeleteContact?id={id}");
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<Contact>>($"{_baseUrl}/api/Contact/GetAllContact");
        }

        public async Task<Contact> GetById(long id)
        {
            return await _httpClient.GetFromJsonAsync<Contact>($"{_baseUrl}/api/Contact/GetByIdContact?id={id}");
        }

        public async Task<List<Contact>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Contact/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _httpClient.GetFromJsonAsync<List<Contact>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Contact/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }
    }
}
