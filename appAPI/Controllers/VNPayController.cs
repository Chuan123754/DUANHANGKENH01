using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        //private readonly IVnPayService _vnPayService;

        //public VNPayController(IVnPayService vnPayService)
        //{
        //    _vnPayService = vnPayService;
        //}

        //[HttpPost]
        //public IActionResult CreatePaymentUrlVnpay([FromBody] PaymentInformationModel model)
        //{
        //    var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
        //    return Ok(url);
        //}


        //[HttpGet]
        //public IActionResult PaymentCallbackVnpay()
        //{
        //    var response = _vnPayService.PaymentExecute(Request.Query);
        //    return Ok(response); // Sử dụng JsonResult để trả về JSON
        //}

    }
}
