using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class TagsServices : ITagsServices
    {
        HttpClient client;
        private readonly string _baseUrl;
        public TagsServices(IConfiguration configuration)
        {
            client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task<List<Post_tags>> GetTagByPostId(long postId)
        {
            string requestURL = $"{_baseUrl}/api/Tags/GetTagByPostId?postId={postId}";
            var response = await client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Post_tags>>(response);
        }
        public async Task Create(Tags tag)
        {
            var response = await client.PostAsJsonAsync($"{_baseUrl}/api/Tags/add", tag);
            if (response.IsSuccessStatusCode)
            {
                // Lấy lại tag đã được tạo từ response
                 await response.Content.ReadFromJsonAsync<Tags>();
            }
             // Trả về null nếu không thành công
        }

        public async Task Delete(long id)
        {
            await client.DeleteAsync($"{_baseUrl}/api/Tags/delete?id={id}");
        }

        public async Task<Tags> Details(long id)
        {
            return await client.GetFromJsonAsync<Tags>($"{_baseUrl}/api/Tags/details?id={id}");
        }

        public async Task<List<Tags>> GetAll()
        {
            return await client.GetFromJsonAsync<List<Tags>>($"{_baseUrl}/api/Tags/show");
        }

        public async Task Update(Tags tag)
        {
            await client.PutAsJsonAsync($"{_baseUrl}/api/Tags/edit", tag);
        }
    }
}
