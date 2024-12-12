using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;
using ViewsFE.Models.DTO;

namespace ViewsFE.Services
{
    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OrderTrackingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<OrderTrackingDTO> GetBy(long Id)
        {
            string requestURL = $"{_baseUrl}/api/OrderTrackings/GetOrderTracking?orderId={Id}";
            var response = await _httpClient.GetStringAsync(requestURL);

            // Deserialize dữ liệu API trả về
            var tracking = JsonConvert.DeserializeObject<OrderTrackingDTO>(response);

            // Ánh xạ dữ liệu từ order_trackings sang OrderTrackingDTO
            var dto = new OrderTrackingDTO
            {
                OrderId = Id,
                SellerName = tracking.SellerName,
                BuyerName = tracking.BuyerName,
                Address = tracking.Address,
                Status = tracking.OrderTrackings.FirstOrDefault()?.Status,                
                Note = tracking.Note,
                Created_at = tracking.Created_at,
                OrderTrackings = tracking.OrderTrackings.Select(t => new OrderTrackingItem
                {
                    Id = t.Id,
                    Status = t.Status,
                    Note = t.Note,
                    CreatedAt = t.CreatedAt
                }).ToList(),
                Products = tracking.Products.Select(p => new ProductItem
                {
                    SKU = p.SKU,
                    Color = p.Color,
                    Size = p.Size,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    TotalDiscount = p.TotalDiscount,
                    Image = p.Image
                }).ToList(),
                TypePayment=tracking.TypePayment,
                TotalPrincipal = tracking.TotalPrincipal,
                TotalAmount = tracking.TotalAmount,
                TotalDiscount = tracking.TotalDiscount,
                FeeShipping = tracking.FeeShipping,
                TotalMoney = tracking.TotalMoney,
                Created_by = tracking.Created_by
            };

            return dto;
        }



        public async Task<List<Orders>> GetAllOrders()
        {
            string requestURL = $"{_baseUrl}/api/Orders/All-Orders";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task<string> AddOrderTracking(order_trackings newOrderTracking)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/OrderTrackings/AddOrderTracking", newOrderTracking);

            if (response.IsSuccessStatusCode)
            {
                return "Thêm mới thành công.";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Lỗi khi thêm mới: {error}";
            }
        }

        public async Task<string> UpdateOrderTracking(long id, order_trackings updatedOrderTracking)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/OrderTrackings/UpdateOrderTracking/{id}", updatedOrderTracking);

            if (response.IsSuccessStatusCode)
            {
                return "Cập nhật thành công.";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Lỗi khi cập nhật: {error}";
            }
        }

        public async Task<string> DeleteOrderTracking(long id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/OrderTrackings/DeleteOrderTracking/{id}");

            if (response.IsSuccessStatusCode)
            {
                return "Xóa thành công.";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Lỗi khi xóa: {error}";
            }
        }
    }

}
