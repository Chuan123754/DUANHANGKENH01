using ViewsFE.IServices;
using ViewsFE.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using Org.BouncyCastle.Ocsp;

namespace ViewsFE.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public CategoriesServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); // Lấy URL từ appsettings.json
        }

        public async Task Create(Categories c)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Category/add-category", c);
        }
        public async Task CreateTypePost(Categories c)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Category/add-category-post", c);
        }

        public async Task CreateTypeProject(Categories c)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Category/add-category-project", c);
        }

        public async Task CreateTypeProduct(Categories c)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Category/add-category-product", c);
        }
        public async Task<List<Post_categories>> GetCategoryByPosstId(long postId)
        {
            string requetsURL = $"{_baseUrl}/api/Category/GetCategoryByPostId?postId={postId}";
            var response = await _client.GetStringAsync(requetsURL);
            return JsonConvert.DeserializeObject<List<Post_categories>>(response);
        }
        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Category/delete-category/{id}");
        }

        public async Task<Categories> Details(long id)
        {
            return await _client.GetFromJsonAsync<Categories>($"{_baseUrl}/api/Category/details/{id}");
        }

        public async Task<List<Categories>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Categories>>($"{_baseUrl}/api/Category/show");
        }

        public async Task<List<Categories>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Category/get-by-type?type={type}&pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Categories>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string type, string searchTerm)
        {
            var url = $"{_baseUrl}/api/Category/Get-Total-Count?type={type}&searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

        public async Task Update(Categories c)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Category/edit-category", c);
        }

        public async Task<List<Categories>> GetAllType(string type)
        {
            return await _client.GetFromJsonAsync<List<Categories>>($"{_baseUrl}/api/Category/GetAllType?type={type}");
        }

        public async Task<bool> CheckSlug(string slug)
        {
            var response = await _client.GetAsync($"{_baseUrl}/api/Category/checkslug?slug={slug}");
            response.EnsureSuccessStatusCode();
            return bool.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> CheckSlugForUpdate (long cateId,string slug)
        {
            try
            {
                // Gửi request đến API
                var response = await _client.GetFromJsonAsync<ApiResponse>($"{_baseUrl}/api/Category/check-slug-for-update?slug={slug}&cateid={cateId}");

                // Nếu response hợp lệ, trả về giá trị `IsUnique`
                return response?.IsUnique ?? false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling CheckSlugForUpdate API: {ex.Message}");
                return false; // Trả về false trong trường hợp lỗi
            }
        }
        public class ApiResponse
        {
            public bool IsUnique { get; set; }
        }
    }
}
