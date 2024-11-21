using appAPI.Migrations;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http;
using System.Text.Json;
using ViewsFE.IServices;
using ViewsFE.Models;
using ViewsFE.Models.DTO;

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

        public async Task<long> CreatePage(Product_Posts post, List<long> tagIds)
        {
            // Chuyển đổi danh sách tagIds và category thành chuỗi query string
            var tagIdsString = string.Join("&", tagIds.Select(id => $"tagIds={id}"));

            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-page?{tagIdsString}", post);
            response.EnsureSuccessStatusCode(); // Kiểm tra phản hồi

            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
        }

        public async Task<long> CreatePost(Product_Posts post, List<long> tagIds, List<long> category)
        {
            // Chuyển đổi danh sách tagIds và category thành chuỗi query string
            var tagIdsString = string.Join("&", tagIds.Select(id => $"tagIds={id}"));
            var categoriesString = string.Join("&", category.Select(c => $"cate={c}"));

            // Gửi yêu cầu POST với các tham số cần thiết
            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-post?{tagIdsString}&{categoriesString}", post);
            // Kiểm tra phản hồi
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();

            // Trả về Post_Id
            return responseData.Post_Id;
        }
        public class ResponseMessage
        {
            public long Post_Id { get; set; }
        }
        public async Task<long> CreateProduct(Product_Posts post, List<long> tagIds, List<long> category)
        {
            // Chuyển đổi danh sách tagIds và category thành chuỗi query string
            var tagIdsString = string.Join("&", tagIds.Select(id => $"tagIds={id}"));
            var categoriesString = string.Join("&", category.Select(c => $"cate={c}"));

            // Gửi yêu cầu POST với các tham số cần thiết
            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Product_Post/Create-product?{tagIdsString}&{categoriesString}", post);
            response.EnsureSuccessStatusCode(); // Kiểm tra phản hồi
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
        }

        public async Task<long> CreateProject(Product_Posts post, List<long> tagIds, List<long> category)
        {
            var tagIdsString = string.Join("&", tagIds.Select(id => $"tagIds={id}"));
            var categoriesString = string.Join("&", category.Select(c => $"cate={c}"));
            var url = $"{_baseUrl}/api/Product_Post/Create-project?{tagIdsString}&{categoriesString}";
            var response = await _client.PostAsJsonAsync(url, post);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
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
            // Gọi API và lấy dữ liệu
            return await _client.GetFromJsonAsync<Product_Posts>($"{_baseUrl}/api/Product_Post/GetByIdAndType?id={id}&type={type}");
        }

        public async Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm)
        {
            var uri = $"{_baseUrl}/api/Product_Post/get-by-type?type={type}&pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            return await _client.GetFromJsonAsync<List<Product_Posts>>(uri);
        }

        public async Task<int> GetTotalCountAsync(string type, string searchTerm)
        {
            var url = $"{_baseUrl}/api/Product_Post/Get-Total-Count?type={type}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }
        public async Task<long> Update(Product_Posts post, List<long> tagIds)
        {
            var tagIdsString = string.Join("&", tagIds.Select(id => $"tagIds={id}"));
            var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Product_Post/Edit-post?{tagIdsString}", post);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
        }

        public async Task<long> Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds)
        {
            var tagIdsString = string.Join("&", tagIds.Select(id => $"tagIds={id}"));
            var categoriesString = string.Join("&", categoryIds.Select(c => $"categoryIds={c}"));
            var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Product_Post/Edit-posttagscate?{tagIdsString}&{categoriesString}", post);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseData.Post_Id;
        }
    }
}
