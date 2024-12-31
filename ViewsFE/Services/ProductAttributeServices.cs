using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ViewsFE.DTO;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ProductAttributeServices : IProductAttributeServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public ProductAttributeServices(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Product_Attributes productAttribute)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/ProductAttributes/CreateProductAttrubute", productAttribute);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/ProductAttributes/DeleteProductAttribute?id={id}");
        }

        public async Task DeleteCung(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/ProductAttributes/DeleteProductAttributeCung?id={id}");
        }

        public async Task<List<Product_Attributes>> GetAllProductAttributes()
        {
            string requestURL = $"{_baseUrl}/api/ProductAttributes/GetAllProductAttributes";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
        }

        public async Task<List<Product_Attributes>> GetAllProductAttributesDelete()
        {
            string requestURL = $"{_baseUrl}/api/ProductAttributes/GetAllProductAttributesDelete";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
        }

        public async Task<List<Product_Attributes>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            string requestURL = $"{_baseUrl}/api/ProductAttributes/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={Uri.EscapeDataString(searchTerm)}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);

        }

        public async Task<Product_Attributes> GetProductAttributesById(long id)
        {
            string requestURL = $"{_baseUrl}/api/ProductAttributes/GetProductAttributeById?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Product_Attributes>(response);
        }

        public async Task<List<Product_Attributes>> GetProductAttributesByPostId(long id)
        {
            string requestURL = $"{_baseUrl}/api/ProductAttributes/GetProductAttributesByPostId?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
        }
        public async Task<List<Product_Attributes>> GetProductAttributesByPostIdClient(long id)
        {
            string requestURL = $"{_baseUrl}/api/ProductAttributes/GetProductAttributesByPostIdClient?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Product_Attributes>>(response);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            var url = $"{_baseUrl}/api/ProductAttributes/Get-Total-Count?searchTerm={Uri.EscapeDataString(searchTerm)}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

        public async Task Restore(long id)
        {
            var response = await _client.PutAsync($"{_baseUrl}/api/ProductAttributes/Restore-productattribute?id={id}", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse>();
                throw new Exception(error?.Message ?? "Có lỗi xảy ra");
            }
        }

        public class ApiResponse
        {
            public string Message { get; set; }
        }

        public async Task Update(Product_Attributes productAttribute, long id)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/ProductAttributes/UpdateProductAttrubutes?id={id}", productAttribute);
        }
    }
}
