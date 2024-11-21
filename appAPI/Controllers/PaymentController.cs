using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoRepository _momoRepository;

        public PaymentController(IMomoRepository momoRepository)
        {
            _momoRepository = momoRepository;
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] OrderInfoModel model)
        {
            if (string.IsNullOrEmpty(model.FullName) ||
                string.IsNullOrEmpty(model.OrderInfo) ||
                model.Amount <= 0)
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

    }

}
