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
        public async Task Create(Product_variants productVariants)
        {
             await _client.PostAsJsonAsync("https://localhost:7011/api/ProductVarians/CreateProduct",productVariants);
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
