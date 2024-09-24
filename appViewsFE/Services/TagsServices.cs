using AppViews.IServices;
using appViews.Models;

namespace AppViews.Services
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
            await client.PostAsJsonAsync("https://localhost:7015/api/Tags/add", tag);
        }

        public async Task Delete(long id)
        {
            await client.DeleteAsync("$https://localhost:7015/api/Tags/delete?id={id}");
        }

        public async Task<Tags> Details(long id)
        {
            return await client.GetFromJsonAsync<Tags>($"https://localhost:7015/api/Tags/details?id={id}");
        }

        public async Task<List<Tags>> GetAll()
        {
            return await client.GetFromJsonAsync<List<Tags>>("https://localhost:7015/api/Tags/show");
        }

        public async Task Update(Tags tag)
        {
            await client.PutAsJsonAsync("https://localhost:7015/api/Tags/edit", tag);
        }
    }
}
