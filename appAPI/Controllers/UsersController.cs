using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        APP_DATA_DATN context;
        public UsersController(APP_DATA_DATN _context)
        {
            context = _context;
        }

        [HttpGet("Users-get")]
        public IActionResult Get()
        {
            return Ok(context.Users.ToList());
        }

        [HttpGet("Users-get-id")]
        public IActionResult Get(long id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpGet("Users-get-email")]
        public IActionResult GetByEmail(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("Email không tồn tại");
            }
            return Ok(user);
        }

        [HttpGet("Users-get-phone")]
        public IActionResult GetByPhone(string phone)
        {
            var user = context.Users.FirstOrDefault(u => u.Phone == phone);
            if (user == null)
            {
                return NotFound("Số điện thoại không tồn tại");
            }
            return Ok(user);
        }

        [HttpPost("Users-post")]
        public ActionResult Post(Users user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("Users-put")]
        public IActionResult Put(Users user)
        {
            var item = context.Users.Find(user.Id);
            if (item == null)
            {
                return NotFound("User not found");
            }
            item.Name = user.Name;
            item.Phone = user.Phone;
            item.Email = user.Email;
            item.EmailVerifiedAt = user.EmailVerifiedAt;
            item.Password = user.Password;
            item.RememberToken = user.RememberToken;
            item.Address = user.Address;
            item.Created_at = user.Created_at;
            item.Updated_at = user.Updated_at;
            context.SaveChanges();
            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpDelete("Users-delete")]
        public IActionResult Delete(long id)
        {
            var delete = context.Users.Find(id);
            if (delete == null)
            {
                return NotFound("User not found");
            }

            context.Remove(delete);
            context.SaveChanges();
            return Ok(new { message = "Xóa thành công" });
        }


    }
}
