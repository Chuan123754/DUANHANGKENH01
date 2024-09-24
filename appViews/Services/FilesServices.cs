using AppViews.IServices;
using appViews.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppViews.Services
{
    public class FilesServices : FilesIServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;  

        public FilesServices(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); 
        }

        public async Task Delete(long id)
        {
            string requestURL = $@"{_baseUrl}/api/Files/delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }

        public async Task<List<Files>> GetAll()
        {
            string requestURL = $@"{_baseUrl}/api/Files/get-all-files";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Files>>(response);
        }

        public async Task<Files> GetById(long id)
        {
            string requestURL = $@"{_baseUrl}/api/Files/get-details?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Files>(response);
        }

        public async Task<List<Files>> Search(string keyword)
        {
            string requestURL = $@"{_baseUrl}/api/Files/search?query={keyword}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Files>>(response);
        }

        public async Task<object> Upload(IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
                string requestURL = $@"{_baseUrl}/api/Files/upload";
                var response = await _httpClient.PostAsync(requestURL, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject(responseData);
                }
                else
                {
                    throw new HttpRequestException("Upload thất bại.");
                }
            }
        }
    }
}
