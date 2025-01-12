using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderVouchersController : ControllerBase
    {
        private readonly IRepository<Order_Vouchers> _orderVoucherRepository;

        public OrderVouchersController(IRepository<Order_Vouchers> orderVoucherRepository)
        {
            _orderVoucherRepository = orderVoucherRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderVoucherRepository.GetAll());
        }
        [HttpGet("GetByIdOrderAndIdVoucher")]
        public IActionResult Get(long idOrder ,long idVoucher)
        {
            var allVoucherOrders = _orderVoucherRepository.GetAll();
            var voucherOrder = allVoucherOrders.Where(o=>o.OrderId==idOrder && o.VoucherId==idVoucher ).FirstOrDefault();
            return Ok(voucherOrder);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var orderVoucher = _orderVoucherRepository.GetById(id);
            if (orderVoucher == null) return NotFound("Order voucher not found");
            return Ok(orderVoucher);
        }

        [HttpPost("Create")]
        public IActionResult Post(Order_Vouchers orderVoucher)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _orderVoucherRepository.Add(orderVoucher);
            return Ok("Order voucher added successfully");
        }

        [HttpPut]
        public IActionResult Put(Order_Vouchers orderVoucher)
        {
            var existing = _orderVoucherRepository.GetById(orderVoucher.Id);
            if (existing == null) return NotFound("Order voucher not found");

            existing.OrderId = orderVoucher.OrderId;
            existing.VoucherId = orderVoucher.VoucherId;
            existing.AppliedAt = orderVoucher.AppliedAt;

            _orderVoucherRepository.Update(existing);
            return Ok("Order voucher updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var orderVoucher = _orderVoucherRepository.GetById(id);
            if (orderVoucher == null) return NotFound("Order voucher not found");
            _orderVoucherRepository.Remove(orderVoucher);
            return Ok("Order voucher deleted successfully");
        }
    }

}
