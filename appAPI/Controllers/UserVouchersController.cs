using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVouchersController : ControllerBase
    {
        private readonly IRepository<UserVouchers> _userVoucherRepository;

        public UserVouchersController(IRepository<UserVouchers> userVoucherRepository)
        {
            _userVoucherRepository = userVoucherRepository;
        }

        // Lấy tất cả UserVouchers
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userVoucherRepository.GetAll());
        }

        // Lấy UserVoucher theo Id
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var userVoucher = _userVoucherRepository.GetById(id);
            if (userVoucher == null) return NotFound("UserVoucher not found");
            return Ok(userVoucher);
        }

        [HttpGet("byVoucher/{voucherId}")]
        public IActionResult GetByVoucherId(long voucherId)
        {
            // Lấy danh sách UserVouchers từ repository theo VoucherId
            var userVouchers = _userVoucherRepository.GetAll()
                                                     .Where(uv => uv.VoucherId == voucherId)
                                                     .ToList();

            if (userVouchers == null || !userVouchers.Any())
            {
                return NotFound();
            }

            return Ok(userVouchers);
        }

        [HttpGet("byVoucherAndUser/{voucherId}/{userId}")]
        public IActionResult GetByVoucherIdAndUserId(long voucherId, long userId)
        {
            // Tìm UserVoucher theo VoucherId và UserId
            var userVoucher = _userVoucherRepository.GetAll()
                                                     .FirstOrDefault(uv => uv.VoucherId == voucherId && uv.UserId == userId);

            // Nếu không tìm thấy, trả về lỗi 404
            if (userVoucher == null)
            {
                return NotFound("UserVoucher not found for the given VoucherId and UserId");
            }

            // Nếu tìm thấy, trả về kết quả
            return Ok(userVoucher);
        }


        // Thêm mới một UserVoucher
        [HttpPost("post")]
        public IActionResult Post([FromBody] UserVouchers userVoucher)
        {

            // Thêm UserVoucher mới
            _userVoucherRepository.Add(userVoucher);
            return Ok("UserVoucher added successfully");
        }

        // Cập nhật một UserVoucher
        [HttpPut("put")]
        public IActionResult Put(UserVouchers userVoucher)
        {
            var existingUserVoucher = _userVoucherRepository.GetById(userVoucher.Id);
            if (existingUserVoucher == null) return NotFound("UserVoucher not found");

            // Cập nhật các trường cần thiết
            existingUserVoucher.UserId = userVoucher.UserId;
            existingUserVoucher.VoucherId = userVoucher.VoucherId;
            existingUserVoucher.IsApplied = userVoucher.IsApplied;
            existingUserVoucher.AppliedAt = userVoucher.AppliedAt;
            existingUserVoucher.Create_at = userVoucher.Create_at;
            existingUserVoucher.Update_at = userVoucher.Update_at;

            _userVoucherRepository.Update(existingUserVoucher);
            return Ok("UserVoucher updated successfully");
        }

        // Xóa UserVoucher theo Id
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var userVoucher = _userVoucherRepository.GetById(id);
            if (userVoucher == null) return NotFound("UserVoucher not found");

            _userVoucherRepository.Remove(userVoucher);
            return Ok("UserVoucher deleted successfully");
        }
    }
}
