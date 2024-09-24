using AppViews.IServices;
using appViews.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppViews.Services
{
    public class OptionServices : IOptionsServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl; // URL base được lấy từ file cấu hình

        public OptionServices(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); // Lấy URL từ cấu hình
        }

        public async Task Create(Options op)
        {
            string requestURL = $@"{_baseUrl}/api/Options/add";
            await _httpClient.PostAsJsonAsync(requestURL, op);
        }

        public async Task Delete(long id)
        {
            string requestURL = $@"{_baseUrl}/api/Options/delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }

        public async Task<Options> Details(long id)
        {
            string requestURL = $@"{_baseUrl}/api/Options/details?id={id}";
            return await _httpClient.GetFromJsonAsync<Options>(requestURL);
        }

        public async Task<List<Options>> GetAll()
        {
            string requestURL = $@"{_baseUrl}/api/Options/show";
            return await _httpClient.GetFromJsonAsync<List<Options>>(requestURL);
        }

        public async Task Update(Options op)
        {
            string requestURL = $@"{_baseUrl}/api/Options/edit";
            await _httpClient.PutAsJsonAsync(requestURL, op);
        }
    }
}
