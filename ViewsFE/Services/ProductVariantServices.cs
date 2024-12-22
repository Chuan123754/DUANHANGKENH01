//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using ViewsFE.IServices;
//using ViewsFE.Models;

//namespace ViewsFE.Services
//{
//    public class ProductVariantServices : IProductVariantServices
//    {
//        private readonly HttpClient _client;
//        private readonly string _baseUrl;

//        public ProductVariantServices(HttpClient client, IConfiguration configuration)
//        {
//            _client= client;
//            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); // Lấy URL từ appsettings.json
//        }
//        public async Task<long> Create(Product_variants productVariants)
//        {
//             var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/ProductVarians/CreateProduct", productVariants);
//            if (response.IsSuccessStatusCode)
//            {
//                // Đọc phản hồi dưới dạng chuỗi và sau đó chuyển đổi thành long
//                var idString = await response.Content.ReadAsStringAsync();
//                if (long.TryParse(idString, out long id))
//                {
//                    return id;
//                }
//                else
//                {
//                    throw new Exception("Phản hồi không hợp lệ, không thể chuyển đổi thành ID.");
//                }
//            }

//            throw new Exception("Không thể tạo sản phẩm.");

//        }

//        public async Task Delete(long id)
//        {
//            await _client.DeleteAsync($"{_baseUrl}/api/ProductVarians/{id}");
//        }

//        public async Task<List<Product_variants>> GetAllProductVarians()
//        {
//            string requestURL = $"{_baseUrl}/api/ProductVarians/GetAllProduct";
//            var response = await _client.GetStringAsync(requestURL);
//            return JsonConvert.DeserializeObject<List<Product_variants>>(response);
//        }  public async Task<List<Product_Posts>> GetAllProductPosts()
//        {
//            string requestURL = $"{_baseUrl}api/Product_Post/Get-All";
//            var response = await _client.GetStringAsync(requestURL);
//            return JsonConvert.DeserializeObject<List<Product_Posts>>(response);
//        }

//        public async Task<Product_variants> GetProductVariantsById(long id)
//        {
//            string requestURL = $"{_baseUrl}/api/ProductVarians/GetProductById?id={id}";
//            var response = await _client.GetStringAsync(requestURL);
//            return JsonConvert.DeserializeObject<Product_variants>(response);
//        }

//        public async Task Update(Product_variants productVariants, long id)
//        {
//            await _client.PutAsJsonAsync($"{_baseUrl}/api/ProductVarians/UpdateProduct?id={id}", productVariants);
//        }

//        public async Task<int> GetTotalCountAsync(string type, string searchTerm)
//        {
//            var url = $"{_baseUrl}/api/Product_Post/Get-Total-Count?type={type}&searchTerm={Uri.EscapeDataString(searchTerm)}";
//            var response = await _client.GetAsync(url);
//            response.EnsureSuccessStatusCode();

//            var count = await response.Content.ReadFromJsonAsync<int>();
//            return count;
//        }

//        public async Task<List<Product_Attributes>> GetProductAttributesByProductVarianId(long productVariantId)
//        {
//            var response = await _client.GetStringAsync($"{_baseUrl}/api/ProductAttributes/GetByProductVariantId?productVariantId={productVariantId}");
//            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
//        }

//        public async Task<Product_variants> FindVariant(long postId, byte textileTechnologyId, byte styleId, byte materialId)
//        {
//            string requestURL = $"{_baseUrl}/api/ProductVarians/FindVariant?postId={postId}&textileTechnologyId={textileTechnologyId}&styleId={styleId}&materialId={materialId}";
//            var response = await _client.GetAsync(requestURL);
//            if (response.IsSuccessStatusCode)
//            {
//                var responseData = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<Product_variants>(responseData);
//            }
//            return null;
//        }

//        public async Task<int> GetTotal()
//        {
//            var url = $"{_baseUrl}/api/ProductVarians/Get-Total-Product";
//            var response = await _client.GetAsync(url);
//            response.EnsureSuccessStatusCode();

//            var count = await response.Content.ReadFromJsonAsync<int>();
//            return count;
//        }
//    }
//}
