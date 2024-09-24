using appViews.Models;
using appViews.IServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace appViews.Services
{
    public class AttributesService : IAttributesServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public AttributesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task Create(Attributes at)
        {
            string requestURL = $"https://localhost:7011/api/Attributes/Attributes-post";
            await _httpClient.PostAsJsonAsync(requestURL, at);
        }

        public async Task Delete(long id)
        {
            string requestURL = $"{_baseUrl}/api/Attributes/Attributes-delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }

        public async Task<List<Attributes>> GetAll()
        {
            
            
            
            
            string requestURL = $"https://localhost:7011/api/Attributes/Attributes-get";
            return await _httpClient.GetFromJsonAsync<List<Attributes>>(requestURL);
        }

        public async Task<Attributes> GetById(long id)
        {
            string requestURL = $"{_baseUrl}/api/Attributes/Attributes-get-id?id={id}";
            return await _httpClient.GetFromJsonAsync<Attributes>(requestURL);
        }

        public async Task Update(Attributes at)
        {
            string requestURL = $"{_baseUrl}/api/Attributes/Attributes-put";
            await _httpClient.PutAsJsonAsync(requestURL, at);
        }
        public async Task<List<Attributes>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Attributes/search?query={keyword}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Attributes>>(response);
        }
    }
}
