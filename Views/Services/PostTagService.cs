using AppViews.IServices;
using Views.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppViews.Services
{
    public class PostTagService : IPostTagService
    {
        private readonly HttpClient _httpClient;
        public PostTagService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post_tags>> GetAll()
        {
            string requestURL = "https://localhost:7015/api/PostTags/posttags-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Post_tags>>(response);
        }

        public async Task<Post_tags> GetById(long id)
        {
            string requestURL = $"https://localhost:7015/api/PostTags/posttags-get-id?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Post_tags>(response);
        }

        public async Task Create(Post_tags postTag)
        {
            // Bao gồm các trường cần thiết
            var postTagToSend = new
            {
                Id = postTag.Id,
                Post_Id = postTag.Post_Id,
                Tag_Id = postTag.Tag_Id,                
            };

            string requestURL = "https://localhost:7015/api/PostTags/posttags-post";
            var jsonContent = JsonConvert.SerializeObject(postTagToSend);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status code {response.StatusCode} and message: {errorContent}");
            }
        }






        public async Task Update(Post_tags postTag)
        {
            var postTagToSend = new
            {
                Id = postTag.Id,
                Post_Id = postTag.Post_Id,
                Tag_Id = postTag.Tag_Id,
                Posts = new
                {
                    id = 0,
                    title = "string",
                    slug = "string",
                    status = "string",
                    authorId = 0,
                    type = "string",
                    post_date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    created_by = 0,
                    updated_by = 0,
                    deleted_at = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    created_at = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    updated_at = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                },
                Tags = new
                {
                    id = 0,
                    title = "string",
                    slug = "string",
                    type = "string",
                    description = "string",
                    created_at = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    updated_at = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                }
            };

            string requestURL = "https://localhost:7015/api/PostTags/posttags-put";
            var jsonContent = JsonConvert.SerializeObject(postTagToSend);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status code {response.StatusCode} and message: {errorContent}");
            }
        }


        public async Task Delete(long id)
        {
            string requestURL = $"https://localhost:7015/api/PostTags/posttags-delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }
    }
}
