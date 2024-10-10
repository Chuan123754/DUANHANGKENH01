using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
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
        public async Task<List<Textile_technology>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Category/search?query={keyword}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Textile_technology>>(response);
        }
        public async Task Update(Textile_technology t)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Textile_technology", t);
        }
    }
}
