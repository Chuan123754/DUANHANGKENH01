using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ProductVariantServices : IProductVariantServices
    {
        private readonly HttpClient _client;

        public ProductVariantServices(HttpClient client)
        {
            _client= client;
        }
        public async Task<long> Create(Product_variants productVariants)
        {
             var response = await _client.PostAsJsonAsync("https://localhost:7011/api/ProductVarians/CreateProduct",productVariants);
            if (response.IsSuccessStatusCode)
            {
                // Đọc phản hồi dưới dạng chuỗi và sau đó chuyển đổi thành long
                var idString = await response.Content.ReadAsStringAsync();
                if (long.TryParse(idString, out long id))
                {
                    return id;
                }
                else
                {
                    throw new Exception("Phản hồi không hợp lệ, không thể chuyển đổi thành ID.");
                }
            }

            throw new Exception("Không thể tạo sản phẩm.");

        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"https://localhost:7011/api/ProductVarians/{id}");
        }

        public async Task<List<Product_variants>> GetAllProductVarians()
        {
            string requestURL = "https://localhost:7011/api/ProductVarians/GetAllProduct";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_variants>>(response);
        }

        public async Task<Product_variants> GetProductVariantsById(long id)
        {
            string requestURL = $"https://localhost:7011/api/ProductVarians/GetProductById?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Product_variants>(response);
        }

        public async Task Update(Product_variants productVariants, long id)
        {
            await _client.PutAsJsonAsync($"https://localhost:7011/api/ProductVarians/UpdateProduct?id={id}", productVariants);
        }
    }
}
