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

        public async Task<List<Style>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Category/search?query={keyword}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Style>>(response);
        }

        public async Task Update(Style s)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Style/{s.Id}", s);
        }
    }
}
