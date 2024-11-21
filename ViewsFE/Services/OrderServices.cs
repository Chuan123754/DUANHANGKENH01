using iTextSharp.text.pdf.qrcode;
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

        public OrderServices(HttpClient client)
        {
            _client = client;
        }
        public async Task Create(Orders order)
        {
            await _client.PostAsJsonAsync("https://localhost:7011/api/Orders/Create", order);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"https://localhost:7011/api/Orders/Delete?id={id}");
        }

        public async Task<List<Orders>> GetAll()
        {
            string requestURL = "https://localhost:7011/api/Orders/All-Orders";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task<Orders> GetByIdOrders(long id)
        {
            string requestURL = $"https://localhost:7011/api/Orders/OrdersDetails?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Orders> (response);
        }

        public async Task<List<Orders>> GetOrderByIdAdmin(string idAdmin)
        {
            string requestURL = $"https://localhost:7011/api/Orders/GetOrderByIdAdmin?idAdmin={idAdmin}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task<List<Orders>> GetOrderByIdUser(long idUser)
        {
            string requestURL = $"https://localhost:7011/api/Orders/GetOrderByIdUser?idUser={idUser}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public async Task Update(Orders orders , long id)
        {
            await _client.PutAsJsonAsync($"https://localhost:7011/api/Orders/Update?id={id}", orders);
        }

        public async Task<byte[]> ExportInvoice(long orderId)
        {
            var response = await _client.GetAsync($"https://localhost:7011/api/PDF/generate?orderId={orderId}");
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
            var response = await _client.PostAsJsonAsync("https://localhost:7011/api/payment/create-payment-url", new
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

                // Tạo mã QR từ URL
                var qrCodeBase64 = GenerateQrCode(payUrl);

                return new MomoPaymentResponse
                {
                    PayUrl = payUrl,
                    QrCodeBase64 = qrCodeBase64
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

    }
}
