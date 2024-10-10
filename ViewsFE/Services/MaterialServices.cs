using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
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

        public async Task<List<Material>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Category/search?query={keyword}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Material>>(response);
        }

        public async Task Update(Material s)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Material", s);
        }
    }
}
