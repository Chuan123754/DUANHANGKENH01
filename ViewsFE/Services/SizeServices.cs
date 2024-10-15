using Newtonsoft.Json;
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

        public async Task<List<Size>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Category/search?query={keyword}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Size>>(response);
        }

        public async Task Update(Size s)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Size/{s.Id}", s);
        }
    }
}
