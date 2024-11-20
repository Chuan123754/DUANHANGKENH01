using appAPI.IRepository;
using appAPI.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace appAPI.Repository
{
    public class MomoRepository : IMomoRepository
    {
        private readonly MomoOptionModel _options;

        public MomoRepository(IOptions<MomoOptionModel> options)
        {
            _options = options.Value;
        }

        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model)
        {
            var orderId = DateTime.UtcNow.Ticks.ToString();
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
                signature
            };

            var client = new RestClient(_options.MomoApiUrl);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            request.AddJsonBody(requestData);

            Console.WriteLine($"PartnerCode: {_options.PartnerCode}");
            Console.WriteLine($"AccessKey: {_options.AccessKey}");
            Console.WriteLine($"OrderId: {orderId}");
            Console.WriteLine($"Amount: {model.Amount}");
            Console.WriteLine($"OrderInfo: {model.OrderInfo}");

            var response = await client.ExecuteAsync<MomoCreatePaymentResponseModel>(request);
            return response.Data;
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message), "Message cannot be null or empty.");
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException(nameof(secretKey), "SecretKey cannot be null or empty.");

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))).Replace("-", "").ToLower();
        }

    }

}
