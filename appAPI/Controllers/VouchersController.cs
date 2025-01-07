using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IRepository<Vouchers> _voucherRepository;

        public VouchersController(IRepository<Vouchers> voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_voucherRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var voucher = _voucherRepository.GetById(id);
            if (voucher == null) return NotFound("Voucher not found");
            return Ok(voucher);
        }

        [HttpPost("post")]
        public IActionResult Post(Vouchers voucher)
        {
            // Kiểm tra trùng lặp Code khi thêm mới
            var existingVoucher = _voucherRepository.GetAll().FirstOrDefault(v => v.Code == voucher.Code);
            if (existingVoucher != null)
            {
                return BadRequest($"Voucher with code '{voucher.Code}' already exists.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            _voucherRepository.Add(voucher);
            return Ok(voucher);
        }

        [HttpPut("put")]
        public IActionResult Put(Vouchers voucher)
        {
            var existing = _voucherRepository.GetById(voucher.Id);
            if (existing == null) return NotFound("Voucher not found");

            // Kiểm tra trùng lặp Code khi cập nhật (ngoại trừ voucher hiện tại)
            var duplicateVoucher = _voucherRepository.GetAll().FirstOrDefault(v => v.Code == voucher.Code && v.Id != voucher.Id);
            if (duplicateVoucher != null)
            {
                return BadRequest($"Another voucher with code '{voucher.Code}' already exists.");
            }

            existing.Code = voucher.Code;
            existing.Description = voucher.Description;
            existing.Quantity = voucher.Quantity;
            existing.MaxDiscountValue = voucher.MaxDiscountValue;
            existing.Percent = voucher.Percent;
            existing.Status = voucher.Status;
            existing.Start_time = voucher.Start_time;
            existing.End_time = voucher.End_time;
            // Update các thuộc tính khác...
            _voucherRepository.Update(existing);
            return Ok("Voucher updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var voucher = _voucherRepository.GetById(id);
            if (voucher == null) return NotFound("Voucher not found");
            _voucherRepository.Remove(voucher);
            return Ok("Voucher deleted successfully");
        }
    }
}
