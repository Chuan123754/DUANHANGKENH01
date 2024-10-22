using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class TagsServices : ITagsServices
    {
        HttpClient client;
        public TagsServices()
        {
            client = new HttpClient();
        }
        public async Task Create(Tags tag)
        {
            var response = await client.PostAsJsonAsync("https://localhost:7011/api/Tags/add", tag);
            if (response.IsSuccessStatusCode)
            {
                // Lấy lại tag đã được tạo từ response
                 await response.Content.ReadFromJsonAsync<Tags>();
            }
             // Trả về null nếu không thành công
        }

        public async Task Delete(long id)
        {
            await client.DeleteAsync("$https://localhost:7011/api/Tags/delete?id={id}");
        }

        public async Task<Tags> Details(long id)
        {
            return await client.GetFromJsonAsync<Tags>($"https://localhost:7011/api/Tags/details?id={id}");
        }

        public async Task<List<Tags>> GetAll()
        {
            return await client.GetFromJsonAsync<List<Tags>>("https://localhost:7011/api/Tags/show");
        }

        public async Task Update(Tags tag)
        {
            await client.PutAsJsonAsync("https://localhost:7011/api/Tags/edit", tag);
        }
    }
}
