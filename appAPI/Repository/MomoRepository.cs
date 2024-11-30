using appAPI.IRepository;
using appAPI.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

namespace appAPI.Repository
{
    public class MomoRepository : IMomoRepository
    {
        private readonly MomoOptionModel _options;

        public MomoRepository(IOptions<MomoOptionModel> options)
        {
            _options = options.Value;
        }

        public async Task<MomoQueryPaymentResponseModel> QueryTransactionAsync(string orderId)
        {
            var rawData =
                $"partnerCode={_options.PartnerCode}&accessKey={_options.AccessKey}&requestId={orderId}&orderId={orderId}";
            var signature = ComputeHmacSha256(rawData, _options.SecretKey);

            var requestData = new
            {
                partnerCode = _options.PartnerCode,
                accessKey = _options.AccessKey,
                requestId = orderId,
                orderId,
                requestType = "transactionStatus", // Đảm bảo giá trị đúng
                signature
            };

            var client = new RestClient("https://test-payment.momo.vn/v2/gateway/api/query");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            request.AddJsonBody(requestData);

            var response = await client.ExecuteAsync<MomoQueryPaymentResponseModel>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Unable to query transaction status. Response: " + response.Content);
            }

            return response.Data;
        }


        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model)
        {
            var orderId = DateTime.UtcNow.Ticks.ToString(); // Tạo OrderId duy nhất
            var rawData =
                $"partnerCode={_options.PartnerCode}&accessKey={_options.AccessKey}&requestId={orderId}" +
                $"&amount={model.Amount}&orderId={orderId}&orderInfo={model.OrderInfo}" +
                $"&returnUrl={_options.ReturnUrl}&notifyUrl={_options.NotifyUrl}&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.SecretKey);

            var requestData = new
            {
                accessKey = _options.AccessKey,
                partnerCode = _options.PartnerCode,
                requestType = _options.RequestType,
                notifyUrl = _options.NotifyUrl,
                returnUrl = _options.ReturnUrl,
                orderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInfo,
                requestId = orderId,
                extraData = "",
                signature,
                expireTime = DateTime.UtcNow.AddMinutes(15).ToString("yyyy-MM-ddTHH:mm:ss") // Thời gian hết hạn
            };

            var client = new RestClient(_options.MomoApiUrl);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            request.AddJsonBody(requestData);

            int retryCount = 3; // Số lần thử lại khi gọi API thất bại
            while (retryCount > 0)
            {
                var response = await client.ExecuteAsync<MomoCreatePaymentResponseModel>(request);

                if (response.IsSuccessful)
                {
                    LogRequest(rawData, signature, response.Content); // Ghi log thành công
                    return response.Data;
                }

                retryCount--;
                Console.WriteLine($"Retrying... Attempts left: {retryCount}");
            }

            throw new Exception("Failed to call MoMo API after retries.");
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message), "Message cannot be null or empty.");
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException(nameof(secretKey), "SecretKey cannot be null or empty.");

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))).Replace("-", "").ToLower();
        }

        private void LogRequest(string rawData, string signature, string responseContent)
        {
            Console.WriteLine("RawData: " + rawData);
            Console.WriteLine("Signature: " + signature);
            Console.WriteLine("Response: " + responseContent);
        }

        public async Task UpdateOrderStatusAsync(string orderId, string status)
        {
            // Ví dụ cập nhật trạng thái đơn hàng trong cơ sở dữ liệu
            // Giả sử bạn có bảng `Orders` chứa thông tin đơn hàng
            using var connection = new SqlConnection(_options.ConnectionString);
            var query = "UPDATE Orders SET Status = @Status WHERE OrderId = @OrderId";
            await connection.ExecuteAsync(query, new { Status = status, OrderId = orderId });
        }

    }
}
