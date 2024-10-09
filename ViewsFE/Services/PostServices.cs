using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class PostServices : IPostSer
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public PostServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); // Lấy URL từ appsettings.json
        }

        public async Task CreatePage(Product_Posts post)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-page", post);
        }

        public async Task CreatePost(Product_Posts post)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-post", post);
        }

        public async Task CreateProduct(Product_Posts post)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-product", post);
        }
        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}api/Product_Post/Delete-post?id={id}");
        }

        public async Task<List<Product_Posts>> GetAllPage()
        {
            return await _client.GetFromJsonAsync<List<Product_Posts>>($"{_baseUrl}/api/Product_Post/Get-all-page");
        }
        public async Task<List<Product_Posts>> GetAllPost()
        {
            return await _client.GetFromJsonAsync<List<Product_Posts>>($"{_baseUrl}/api/Product_Post/Get-all-post");
        }

        public async Task<List<Product_Posts>> GetAllProduct()
        {
            return await _client.GetFromJsonAsync<List<Product_Posts>>($"{_baseUrl}/api/Product_Post/Get-all-product");
        }

        public async Task<Product_Posts> GetByIdPage(long id)
        {
            return await _client.GetFromJsonAsync<Product_Posts>($"{_baseUrl}/api/Product_Post/BetGyId-page?id={id}");
        }

        public async Task<Product_Posts> GetByIdPost(long id)
        {
            return await _client.GetFromJsonAsync<Product_Posts>($"{_baseUrl}/api/Product_Post/BetGyId-post?id={id}");
        }

        public async Task<Product_Posts> GetByIdProduct(long id)
        {
            return await _client.GetFromJsonAsync<Product_Posts>($"{_baseUrl}/api/Product_Post/BetGyId-product?id={id}");
        }

        public async Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm)
        {
            return await _client.GetFromJsonAsync<List<Product_Posts>>($"{_baseUrl}/api/Product_Post/get-by-type?type={type}&pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={searchTerm}");
        }

        public async Task Update(Product_Posts post)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Product_Post/Edit-post", post);
        }
    }
}
