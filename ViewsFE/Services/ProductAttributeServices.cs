using Newtonsoft.Json;
using ViewsFE.DTO;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ProductAttributeServices : IProductAttributeServices
    {
        private readonly HttpClient _client;

        public ProductAttributeServices(HttpClient client)
        {
            _client = client;
        }
        public async Task Create(Product_Attributes productAttribute)
        {
            await _client.PostAsJsonAsync("https://localhost:7011/api/ProductAttributes/CreateProductAttrubute", productAttribute);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"https://localhost:7011/api/ProductAttributes/DeleteProductAttribute?id={id}");
        }

        public async Task<List<Product_Attributes>> GetAllProductAttributes()
        {
            string requestURL = "https://localhost:7011/api/ProductAttributes/GetAllProductAttributes";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
        }

        public async Task<Product_Attributes> GetProductAttributesById(long id)
        {
            string requestURL = $"https://localhost:7011/api/ProductAttributes/GetProductAttributeById?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Product_Attributes>(response);
        }

        public async Task<List<Product_Attributes>> GetProductAttributesByProductVarianId(long id)
        {
            string requestURL = $"https://localhost:7011/api/ProductAttributes/GetProductAttributesByProductVariantId?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
        }

        public Task<List<Product_Attributes_DTO>> GetVariantByProductVariantId(List<long> variantIds)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Product_Attributes productAttribute, long id)
        {
            await _client.PutAsJsonAsync($"https://localhost:7011/api/ProductAttributes/UpdateProductAttrubutes?id={id}", productAttribute);
        }
    }
}
