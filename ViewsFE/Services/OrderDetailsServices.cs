using appAPI.Repository;
using Newtonsoft.Json;
using System.Net;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class OrderDetailsServices : OrderDetailsIServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public OrderDetailsServices(HttpClient client,  IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task Create(Order_details orderdetails)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/OrderDetails/Create", orderdetails);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/OrderDetails/Delete?id={id}");
        }

        public async Task<List<Order_details>> GetAlldetail()
        {
            string requestURL = $"{_baseUrl}/api/OrderDetails/All";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Order_details>>(response);
        }

        public async Task<Order_details> GetByIdOrderdetails(long id)
        {
            string requestURL = $"{_baseUrl}/api/OrderDetails/Details?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Order_details>(response);
        }

        public async Task<Order_details> GetByOrderIdAndProductAttributeId(long orderId, long productAttributeId)
        {
            string requestURL = $"{_baseUrl}/api/OrderDetails/GetByOrderIdAndProductAttributeId?orderId={orderId}&productAttributeId={productAttributeId}";
            var response = await _client.GetAsync(requestURL);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode(); 
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Order_details>(responseContent);
        }
        
        public async Task<List<Order_details>> GetByTypeAsync(int pageNumber, int pageSize)
        {
            var uri = $"{_baseUrl}/api/OrderDetails/get-by-type?pageNumber={pageNumber}&pageSize={pageSize}";
            return await _client.GetFromJsonAsync<List<Order_details>>(uri);

        }

        public async Task<List<Order_details>> GetOrderDetailsByOrderId(long idOrder)
        {
            string requestURL = $"{_baseUrl}/api/OrderDetails/GetOrderDetailsByOrderId?idOrder={idOrder}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Order_details>>(response);
        }

        public async Task<List<TopSellingProductDtoSV>> GetTop5SellingProductsAsync()
        {          
            string requestURL = $"{_baseUrl}/api/OrderDetails/TopSelling";
            try
            {
                var response = await _client.GetStringAsync(requestURL);
                if (string.IsNullOrEmpty(response))
                {
                    return new List<TopSellingProductDtoSV>();
                }
                var topSellingProducts = JsonConvert.DeserializeObject<List<TopSellingProductDtoSV>>(response);
                return topSellingProducts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching top selling products: {ex.Message}");
                return new List<TopSellingProductDtoSV>();
            }
        }

        public class TopSellingProductDtoSV
        {
            public string ProductName { get; set; } 
            public int TotalQuantitySold { get; set; }
        }
        public async Task<int> GetTotalCountAsync()
        {
            var url = $"{_baseUrl}/api/OrderDetails/Get-Total-Count";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); 

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

        public async Task Update(Order_details orderdetails , long id)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/OrderDetails/Update?id={id}", orderdetails);
        }
    }
}
