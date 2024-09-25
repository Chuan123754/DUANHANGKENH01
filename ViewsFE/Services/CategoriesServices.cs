using AppViews.IServices;
using Views.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

namespace Views.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public CategoriesServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); // Lấy URL từ appsettings.json
        }

        public async Task Create(Categories c)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Category/add-category", c);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Category/delete-category?id={id}");
        }

        public async Task<Categories> Details(long id)
        {
            return await _client.GetFromJsonAsync<Categories>($"{_baseUrl}/api/Category/details?id={id}");
        }

        public async Task<List<Categories>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Categories>>($"{_baseUrl}/api/Category/show");
        }

        public async Task<List<Categories>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Category/search?query={keyword}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Categories>>(response);
        }

        public async Task Update(Categories c)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Category/edit-category", c);
        }
    }
}
