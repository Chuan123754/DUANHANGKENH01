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
            await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Size", c);
        }

        public async Task Delete(long id)
        {   
            await _httpClient.DeleteAsync($"{_baseUrl}/api/Size/{id}");
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<Contact>>($"{_baseUrl}/api/Size");
        }

        public async Task<Contact> GetById(long id)
        {
            return await _httpClient.GetFromJsonAsync<Contact>($"{_baseUrl}/api/Size/{id}");
        }

        public Task<List<Contact>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCountAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
