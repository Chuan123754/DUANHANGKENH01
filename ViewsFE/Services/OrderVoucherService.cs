using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class OrderVoucherService : IOrderVoucherServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OrderVoucherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<bool> Create(Order_Vouchers order_Voucher)
        {
            string requestURL = $"{_baseUrl}/api/OrderVouchers/Create";
            var jsonContent = JsonConvert.SerializeObject(order_Voucher);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create UserVoucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task GetByVoucherIdAndOrderId(long orderId, long voucherId)
        {
            await _httpClient.GetStringAsync($"https://localhost:7011/api/OrderVouchers/GetByIdOrderAndIdVoucher?idOrder={orderId}&idVoucher={voucherId}");
        }
    }
}
