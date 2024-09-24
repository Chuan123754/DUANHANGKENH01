using Views.Models;
using AppViews.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Views.Services
{
    public class CommentServices: ICommentServices
    {
        HttpClient _httpClient;
        public CommentServices(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<List<Comments>> GetAll()
        {
            string requestURL = "https://localhost:7015/api/Comment/GetAll";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Comments>>(response);
        }
        public async Task<Comments> GetById(long id)
        {
            string requestURL = $"https://localhost:7015/api/Comment/GetById?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Comments>(response);
        }

        public async Task Create(Comments comment)
        {
            string requestURL = "https://localhost:7015/api/Comment/Create";
            var jsonContent = JsonConvert.SerializeObject(comment);
            var content = new StringContent(jsonContent, Encoding.UTF8,"application/json");
            await _httpClient.PostAsJsonAsync(requestURL, content);
        }
        public async Task Update( long id, Comments comments)
        {
            string requestURL = $"https://localhost:7015/api/Comment/Update?id={id}";
            var jsonContent = JsonConvert.SerializeObject(comments);
            var content = new StringContent(jsonContent, Encoding.UTF8,"application/json");
            await _httpClient.PostAsync(requestURL, content);
        }
        public async Task Delete(long id)
        {
            string requestURL = $"https://localhost:7015/api/Comment/Delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }
    }
}
