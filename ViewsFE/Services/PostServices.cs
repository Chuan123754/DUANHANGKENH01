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

        public async Task CreatePage(Product_Posts post, List<long> tagIds)
        {
            // Chuyển đổi danh sách tagIds và category thành chuỗi query string
            var tagIdsString = string.Join("&tagIds=", tagIds);

            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-page?{tagIdsString}", post);
            response.EnsureSuccessStatusCode(); // Kiểm tra phản hồi

           await response.Content.ReadFromJsonAsync<Product_Posts>(); // Trả về sản phẩm vừa tạo
        }

        public async Task CreatePost(Product_Posts post, List<long> tagIds, List<long> category)
        {
            // Chuyển đổi danh sách tagIds và category thành chuỗi query string
            var tagIdsString = string.Join("&tagIds=", tagIds);
            var categoriesString = string.Join("&cate=", category);

            // Gửi yêu cầu POST với các tham số cần thiết
            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-post?{tagIdsString}&{categoriesString}", post);
            response.EnsureSuccessStatusCode(); // Kiểm tra phản hồi

            await response.Content.ReadFromJsonAsync<Product_Posts>(); // Trả về sản phẩm vừa tạo
        }

        public async Task CreateProduct(Product_Posts post, List<long> tagIds, List<long> category)
        {
            // Chuyển đổi danh sách tagIds và category thành chuỗi query string
            var tagIdsString = string.Join("&tagIds=", tagIds);
            var categoriesString = string.Join("&cate=", category);

            // Gửi yêu cầu POST với các tham số cần thiết
            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-product?{tagIdsString}&{categoriesString}", post);
            response.EnsureSuccessStatusCode(); // Kiểm tra phản hồi

             await response.Content.ReadFromJsonAsync<Product_Posts>();
        }


        public async Task CreateProject(Product_Posts post, List<long> tagIds, List<long> category)
        {
            var tagIdsString = string.Join("&tagIds=", tagIds);
            var categoriesString = string.Join("&cate=", category);
            var url = $"{_baseUrl}/api/Product_Post/Create-project?tagIds={tagIdsString}&cate={categoriesString}";
            var response = await _client.PostAsJsonAsync(url, post);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Product_Post/Delete-post?id={id}");
        }

        public async Task<List<Product_Posts>> GetAllType(string type)
        {
            return await _client.GetFromJsonAsync<List<Product_Posts>>($"{_baseUrl}/api/Product_Post/Get-all-type?Type={type}");
        }
        
        public async Task<Product_Posts> GetByIdType(long id, string type)
        {
            return await _client.GetFromJsonAsync<Product_Posts>($"{_baseUrl}/api/Product_Post/BetGyIdType?id={id}&type={type}");
        }

        public async Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm)
        {

            var uri = $"{_baseUrl}/api/Product_Post/get-by-type?type={type}&pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Product_Posts>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string type, string searchTerm)
        {
            var url = $"{_baseUrl}/api/Product_Post/Get-Total-Count?type={type}&searchTerm={Uri.EscapeDataString(searchTerm)}";

            // Gọi API và nhận tổng số lượng bài viết
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }


        public async Task Update(Product_Posts post)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Product_Post/Edit-post", post);
        }
    }
}
