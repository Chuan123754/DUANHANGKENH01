using AppViews.IServices;
using appViews.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppViews.Services
{
    public class PostMetaService : IPostMetaService
    {
        private readonly HttpClient _httpClient;
        public PostMetaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post_metas>> GetAll()
        {
            string requestURL = "https://localhost:7015/api/PostMetas/postmetas-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Post_metas>>(response);
        }

        public async Task<Post_metas> GetById(long id)
        {
            string requestURL = $"https://localhost:7015/api/PostMetas/postmetas-get-id?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Post_metas>(response);
        }

        public async Task Create(Post_metas postMeta)
        {
            string requestURL = "https://localhost:7015/api/PostMetas/postmetas-post";
            var jsonContent = JsonConvert.SerializeObject(postMeta);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(requestURL, content);
        }

        public async Task Update(Post_metas postMeta)
        {
            string requestURL = "https://localhost:7015/api/PostMetas/postmetas-put";
            var jsonContent = JsonConvert.SerializeObject(postMeta);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(requestURL, content);
        }

        public async Task Delete(long id)
        {
            string requestURL = $"https://localhost:7015/api/PostMetas/postmetas-delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }
    }
}
