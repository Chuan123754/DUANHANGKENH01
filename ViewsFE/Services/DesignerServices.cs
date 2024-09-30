using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class DesignerServices : IDesignerServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public DesignerServices( HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Designer at)
        {
            string requestURL = $"";
            await _httpClient.PostAsJsonAsync(requestURL, at);
        }

        public async Task Delete(long id)
        {
            string requestURL = $"";
            await _httpClient.DeleteAsync(requestURL);
        }

        public async Task<List<Designer>> GetAll()
        {
            string requestURL = $"";
            return await _httpClient.GetFromJsonAsync<List<Designer>>(requestURL);
        }

        public async Task<Designer> GetById(long id)
        {
            string requestURL = $"";
            return await _httpClient.GetFromJsonAsync<Designer>(requestURL);
        }

        public async Task<List<Designer>> Search(string keyword)
        {
            string requestURL = $"";
           var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Designer>>(response);
        }

        public async Task Update(Designer at)
        {
            string requestURL = $"";
            await _httpClient.PutAsJsonAsync(requestURL, at);   
        }
    }
}
