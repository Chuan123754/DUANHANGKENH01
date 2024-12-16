using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ProductsReturnedService : IProductsReturnedService
    {
        private readonly HttpClient _httpClient;

        public ProductsReturnedService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<Products_Returned>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7011/api/ProductsReturned");

            if (response.IsSuccessStatusCode)
            {
                var productsReturned = await response.Content.ReadFromJsonAsync<List<Products_Returned>>() ?? new List<Products_Returned>();

                // Tải thêm thông tin OrderDetails và ProductAttributes
                foreach (var item in productsReturned)
                {
                    if (item.OrderDetailId > 0)
                    {
                        var orderDetail = await _httpClient.GetFromJsonAsync<Order_details>($"https://localhost:7011/api/OrderDetails/Details?id={item.OrderDetailId}");
                        item.OrderDetails = orderDetail;
                    }
                }
                return productsReturned;
            }
            else
            {
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }


        public async Task<List<Products_Returned>> GetAllWithDetailsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7011/api/ProductsReturned");

            if (response.IsSuccessStatusCode)
            {
                var productsReturned = await response.Content.ReadFromJsonAsync<List<Products_Returned>>() ?? new List<Products_Returned>();

                foreach (var item in productsReturned)
                {
                    if (item.OrderDetailId > 0)
                    {
                        var orderDetail = await _httpClient.GetFromJsonAsync<Order_details>($"https://localhost:7011/api/OrderDetails/GetOrderAndReturnedProductsById?orderId={item.OrderDetailId}");
                        item.OrderDetails = orderDetail;
                    }
                }

                return productsReturned;
            }
            else
            {
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }




        public async Task<Products_Returned?> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/ProductsReturned/{id}");

            return await response.Content.ReadFromJsonAsync<Products_Returned>();
        }

        public async Task<List<Products_Returned>> GetByOrderDetailIdAsync(long orderDetailId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/ProductsReturned/GetByOrderDetailId?orderDetailId={orderDetailId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Products_Returned>>() ?? new List<Products_Returned>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<Products_Returned> CreateAsync(Products_Returned productReturned)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7011/api/ProductsReturned/Post", productReturned);

            if (response.IsSuccessStatusCode)
            {
                var createdProductReturned = await response.Content.ReadFromJsonAsync<Products_Returned>();
                if (createdProductReturned == null)
                {
                    throw new Exception("Không thể đọc dữ liệu sản phẩm vừa được tạo.");
                }
                return createdProductReturned;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> ProcessReturnAsync(long id)
        {
            var response = await _httpClient.PutAsync($"https://localhost:7011/api/ProductsReturned/ProcessReturn/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }public async Task<bool> ProcessbackAsync(long id)
        {
            var response = await _httpClient.PutAsync($"https://localhost:7011/api/ProductsReturned/Processback/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> ProcessReturnQuantityAsync(long orderDetailId, int returnQuantity)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7011/api/ProductsReturned/ProcessReturnQuantity",
                new { OrderDetailId = orderDetailId, ReturnQuantity = returnQuantity }
            );

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> ProcessRefundQuantityAsync(long orderDetailId, int returnQuantity)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7011/api/ProductsReturned/ProcessRefundQuantity",
                new { OrderDetailId = orderDetailId, ReturnQuantity = returnQuantity }
            );

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<Products_Returned?> UpdateAsync(Products_Returned productReturned)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7011/api/ProductsReturned/put", productReturned);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Products_Returned>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> UpdateReturnQuantityAsync(long id, int quantityToStock)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7011/api/ProductsReturned/UpdateReturnQuantity/{id}",
                new { QuantityToStock = quantityToStock }
            );

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }


        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7011/api/ProductsReturned/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
