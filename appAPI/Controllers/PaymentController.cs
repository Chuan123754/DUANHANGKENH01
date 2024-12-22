using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace appAPI.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoRepository _momoRepository;
        private readonly MomoOptionModel _options;
        private readonly OrderIReponsitory _repo;


        public PaymentController(IMomoRepository momoRepository, IOptions<MomoOptionModel> options, OrderIReponsitory repo)
        {
            _momoRepository = momoRepository;
            _options = options.Value; // Gán giá trị từ DI
            _repo = repo;
        }


        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] OrderInfoModel model)
        {
            if (string.IsNullOrEmpty(model.FullName) ||
                string.IsNullOrEmpty(model.OrderInfo) ||
                model.Amount < 0)
            {
                return BadRequest(new { error = "Dữ liệu đầu vào không hợp lệ." });
            }

            try
            {
                var response = await _momoRepository.CreatePaymentAsync(model);
                return Ok(new { payUrl = response.PayUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("query-transaction")]
        public async Task<IActionResult> QueryTransaction(string orderId)
        {
            try
            {
                var response = await _momoRepository.QueryTransactionAsync(orderId);

                // Kiểm tra trạng thái giao dịch dựa trên ResultCode
                if (response.ResultCode == 0)
                {
                    return Ok(new { message = "Giao dịch thành công", response });
                }
                else if (response.ResultCode == 49) // Lỗi giao dịch hết hạn
                {
                    return BadRequest(new { message = "Giao dịch đã hết hạn", response });
                }
                else
                {
                    return BadRequest(new { message = "Giao dịch không thành công", response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message), "Message cannot be null or empty.");
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException(nameof(secretKey), "SecretKey cannot be null or empty.");

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))).Replace("-", "").ToLower();
        }


        [HttpGet("PaymentCallBack")]
        public async Task<IActionResult> PaymentCallBack(
     string partnerCode, string accessKey, string requestId, string amount,
     string orderId, string orderInfo, string orderType, string transId,
     string message, string localMessage, string responseTime, string errorCode,
     string payType, string signature)
        {
            try
            {
                // Xác minh chữ ký từ MoMo
                var rawData =
                    $"partnerCode={partnerCode}&accessKey={accessKey}" +
                    $"&orderId={orderId}&requestId={requestId}" +
                    $"&amount={amount}&orderInfo={orderInfo}" +
                    $"&orderType={orderType}&transId={transId}" +
                    $"&resultCode={errorCode}&message={message}" +
                    $"&payType={payType}&responseTime={responseTime}";

                var expectedSignature = ComputeHmacSha256(rawData, _options.SecretKey);          

                // Tách ID từ orderInfo (ví dụ: "Thanh toán hóa đơn 25" => 25)
                var orderIdFromInfo = ExtractOrderIdFromInfo(orderInfo);       

                // Sử dụng OrderRepository để cập nhật trạng thái đơn hàng
                var order = await _repo.GetByIdOrders(orderIdFromInfo.Value); 
                if (order != null)
                {
                    if (errorCode == "0") // Thanh toán thành công
                    {
                        if(order.CreatedByAdminId == null ) // đơn online
                        {
                            order.Status = "Đã xác nhận";
                        }    
                        else if(order.CreatedByAdminId != null || order.Note=="Tại quầy") // đơn tại quầy
                        {
                            order.Status = "Đã thanh toán";
                        }
                    }
                    else
                    {
                        order.Status = "Chưa thanh toán";
                    }

                    await _repo.Update(order, order.Id); // Gọi phương thức Update của Repository
                }

                // Điều hướng về giao diện với trạng thái thanh toán
                return Redirect($"https://localhost:7277/admin/payment-result?errorCode={errorCode}&message={message}&orderId={orderIdFromInfo}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Hàm để tách ID từ chuỗi orderInfo
        private long? ExtractOrderIdFromInfo(string orderInfo)
        {
            try
            {
                // Tách số từ chuỗi "Thanh toán hóa đơn 25"
                var parts = orderInfo.Split(' ');
                return long.TryParse(parts.Last(), out var id) ? id : (long?)null;
            }
            catch
            {
                return null; // Trả về null nếu không tách được
            }
        }



        [HttpPost("notify")]
        public async Task<IActionResult> Notify([FromBody] MomoNotificationModel notification)
        {
            try
            {
                // Xử lý giá trị mặc định cho extraData nếu null

                // Tiếp tục xử lý bình thường...
                var rawData =
                    $"partnerCode={notification.PartnerCode}&accessKey={_options.AccessKey}" +
                    $"&orderId={notification.OrderId}&requestId={notification.RequestId}" +
                    $"&amount={notification.Amount}&orderInfo={notification.OrderInfo}" +
                    $"&orderType={notification.OrderType}&transId={notification.TransId}" +
                    $"&resultCode={notification.ResultCode}&message={notification.Message}" +
                    $"&payType={notification.PayType}&responseTime={notification.ResponseTime}";

                var expectedSignature = ComputeHmacSha256(rawData, _options.SecretKey);

                if (notification.Signature != expectedSignature)
                {
                    return BadRequest(new { message = "Invalid signature" });
                }

                if (notification.ResultCode == 0)
                {
                    await _momoRepository.UpdateOrderStatusAsync(notification.OrderId, "Paid");
                    return Ok(new { message = "Payment successful" });
                }
                else
                {
                    await _momoRepository.UpdateOrderStatusAsync(notification.OrderId, "Failed");
                    return BadRequest(new { message = "Payment failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }


    }

}
