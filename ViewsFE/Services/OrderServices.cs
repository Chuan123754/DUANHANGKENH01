using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ViewsFE.IServices;
using ViewsFE.Models;
using QRCoder;
using System.Drawing;
using System.IO;
using ViewsFE.Models.DTO;

namespace ViewsFE.Services
{
    public class OrderServices : IOrderIServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public OrderServices(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl"); // Lấy URL từ appsettings.json
        }
        public async Task Create(Orders order)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Orders/Create", order);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"{_baseUrl}/api/Orders/Delete?id={id}");
        }

        public async Task<List<Orders>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/Orders/All-Orders";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task<Orders> GetByIdOrders(long id)
        {
            string requestURL = $"{_baseUrl}/api/Orders/OrdersDetails?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Orders> (response);
        }

        public async Task<Orders> OrdersAddress(long id)
        {
            string requestURL = $"{_baseUrl}/api/Orders/OrdersAddress?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Orders>(response);
        }

        public async Task<List<Orders>> GetOrderByIdAdmin(string idAdmin)
        {
            string requestURL = $"{_baseUrl}/api/Orders/GetOrderByIdAdmin?idAdmin={idAdmin}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task<List<Orders>> GetOrderByIdUser(long idUser)
        {
            string requestURL = $"{_baseUrl}/api/Orders/GetOrderByIdUser?idUser={idUser}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task Update(Orders orders , long id)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Orders/Update?id={id}", orders);
        }

        public async Task UpdateStatus(Orders orders, long id)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Orders/UpdateStatus?id={id}", orders);
        }
        public async Task<byte[]> ExportInvoice(long orderId)
        {
            var response = await _client.GetAsync($"{_baseUrl}/api/PDF/generate?orderId={orderId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync(); 
            }
            else
            {
                throw new Exception("Không thể xuất hóa đơn.");
            }
        }

        // Chức năng gọi API MoMo để lấy URL mã QR
        public async Task<MomoPaymentResponse> CreateMomoPaymentUrl(string fullName, decimal amount, string orderInfo)
        {
            var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/payment/create-payment-url", new
            {
                FullName = fullName,
                Amount = amount,
                OrderInfo = orderInfo
            });

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseContent);
                var payUrl = json["payUrl"]?.ToString();

                if (string.IsNullOrEmpty(payUrl))
                {
                    throw new Exception("API MoMo không trả về URL thanh toán.");
                }

                var qrCodeBase64 = GenerateQrCode(payUrl);

                return new MomoPaymentResponse
                {
                    PayUrl = payUrl,
                    QrCodeBase64 = qrCodeBase64,
                };
            }
            else
            {
                throw new Exception("Không thể tạo mã QR thanh toán MoMo.");
            }
        }



        public string GenerateQrCode(string url)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q); 
                var qrCode = new PngByteQRCode(qrCodeData); 

                // Tạo hình ảnh QR Code dưới dạng Base64
                var qrCodeBytes = qrCode.GetGraphic(20);
                return $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
            }
        }

        public async Task<MomoPaymentResponse> QueryPaymentStatus(string orderId)
        {
            var response = await _client.GetAsync($"{_baseUrl}/api/payment/query-transaction?orderId={orderId}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseContent);

                var resultCode = json["response"]["resultCode"]?.ToString();

                return new MomoPaymentResponse
                {
                    ResultCode = resultCode
                };
            }
            else
            {
                throw new Exception("Không thể kiểm tra trạng thái thanh toán.");
            }
        }

        public async Task<int> GetTotal()
        {
            var url = $"{_baseUrl}/api/Orders/Get-Total-Order";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

        public async Task<int> GetTotalToday()
        {
            var url = $"{_baseUrl}/api/Orders/Get-Total-Order-Today";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }

        public async Task<decimal> GetTotalPiceToday()
        {
            var url = $"{_baseUrl}/api/Orders/Get-Total-Pice-Today";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<decimal>();
            return count;
        }

        public async Task<decimal> GetTotalPiceWeek()
        {
            var url = $"{_baseUrl}/api/Orders/Get-Total-Pice-Week";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<decimal>();
            return count;
        }

        public async Task<decimal> GetTotalPiceMonth()
        {
            var url = $"{_baseUrl}/api/Orders/Get-Total-Pice-Month";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var count = await response.Content.ReadFromJsonAsync<decimal>();
            return count;
        }

        public async Task<Dictionary<int, decimal>> GetTotalRevenuePerYear()
        {
            // Địa chỉ URL của API lấy doanh thu theo từng năm
            var url = $"{_baseUrl}/api/Orders/Get-Total-Revenue-Per-Year";

            // Gửi yêu cầu GET tới API
            var response = await _client.GetAsync(url);

            // Kiểm tra kết quả trả về từ API
            response.EnsureSuccessStatusCode();

            // Đọc dữ liệu trả về dưới dạng Dictionary<int, decimal>
            var revenueData = await response.Content.ReadFromJsonAsync<Dictionary<int, decimal>>();

            return revenueData;
        }

        public async Task<Dictionary<string, int>> GetTotalMonth()
        {
            var url = $"{_baseUrl}/api/Orders/Get-Total-Orders-Per-Month";

            // Gửi yêu cầu GET tới API
            var response = await _client.GetAsync(url);

            // Kiểm tra kết quả trả về từ API
            response.EnsureSuccessStatusCode();

            // Đọc dữ liệu trả về dưới dạng Dictionary<int, decimal>
            var revenueData = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();

            return revenueData;
        }
    }
}
