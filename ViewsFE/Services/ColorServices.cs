using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ColorServices : IColorServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public ColorServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Color c)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Color", c);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Color/{id}");
        }

        public async Task<Color> Details(long id)
        {
            return await _client.GetFromJsonAsync<Color>($"{_baseUrl}/api/Color/{id}");
        }

        public async Task<List<Color>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Color>>($"{_baseUrl}/api/Color");
        }

        public async Task<List<Color>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Category/search?query={keyword}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Color>>(response);
        }

        public async Task Update(Color c)
        {
            var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Color/{c.Id}", c);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception(error);
            }

        }
    }
}
