using Newtonsoft.Json;
using ClientViews.IServices;
using ClientViews.Models;
using System.Net.Http.Json;

namespace ClientViews.Services
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

        public async Task Create(Color color)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Color/create-mau", color);
        }

        public async Task Delete(long id)
        {
            var response = await _client.DeleteAsync($"{_baseUrl}/api/Color/{id}").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception($"Failed to delete color with ID {id}: {error}");
            }
        }

        public async Task<Color> Details(long id)
        {
            var response = await _client.GetAsync($"{_baseUrl}/api/Color/{id}").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception($"Failed to get details of color with ID {id}: {error}");
            }

            return await response.Content.ReadFromJsonAsync<Color>();
        }

        public async Task<List<Color>> GetAll()
        {
            var response = await _client.GetAsync($"{_baseUrl}/api/Color").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception("Failed to get all colors.");
            }

            return await response.Content.ReadFromJsonAsync<List<Color>>();
        }

        public async Task<List<Color>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Color/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception("Failed to get colors by type.");
            }

            return await response.Content.ReadFromJsonAsync<List<Color>>();
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/Color/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";
            var response = await _client.GetAsync(url).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception("Failed to get total count of colors.");
            }

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task Update(Color c)
        {
            var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Color/{c.Id}", c).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {error}");
                throw new Exception($"Failed to update color with ID {c.Id}: {error}");
            }
        }
    }
}
