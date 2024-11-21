using appAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace ViewsFE.Services
{
    public class MomoService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MomoService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> CreatePaymentUrlAsync(string fullName, decimal amount, string orderInfo)
        {
            var momoConfig = _configuration.GetSection("MomoAPI").Get<MomoOptionModel>();

            var orderId = DateTime.UtcNow.Ticks.ToString();
            var rawData =
                $"partnerCode={momoConfig.PartnerCode}&accessKey={momoConfig.AccessKey}&requestId={orderId}" +
                $"&amount={amount}&orderId={orderId}&orderInfo={orderInfo}" +
                $"&returnUrl={momoConfig.ReturnUrl}&notifyUrl={momoConfig.NotifyUrl}&extraData=";

            var signature = ComputeHmacSha256(rawData, momoConfig.SecretKey);

            var requestData = new
            {
                partnerCode = momoConfig.PartnerCode,
                accessKey = momoConfig.AccessKey,
                requestType = momoConfig.RequestType,
                notifyUrl = momoConfig.NotifyUrl,
                returnUrl = momoConfig.ReturnUrl,
                orderId = orderId,
                amount = amount.ToString(),
                orderInfo = orderInfo,
                requestId = orderId,
                extraData = "",
                signature = signature
            };

            var response = await _httpClient.PostAsJsonAsync(momoConfig.MomoApiUrl, requestData);
            var result = await response.Content.ReadFromJsonAsync<MomoCreatePaymentResponseModel>();

            return result?.PayUrl ?? throw new Exception("Failed to get payment URL from MOMO.");
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                return BitConverter.ToString(hmac.ComputeHash(messageBytes)).Replace("-", "").ToLower();
            }
        }
    }

}
