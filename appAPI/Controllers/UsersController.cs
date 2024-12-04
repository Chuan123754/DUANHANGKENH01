using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using System.Text;

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
        [HttpPost("register")]
        public IActionResult Register([FromBody] Users user)
        {
            if (context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest(new { message = "Email đã được sử dụng." });
            }

            user.Password = HashPassword(user.Password!); // Mã hóa mật khẩu
            user.Created_at = DateTime.Now;
            context.Users.Add(user);
            context.SaveChanges();

            return Ok(new { message = "Đăng ký thành công", user });
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Users loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest(new { message = "Dữ liệu đầu vào không hợp lệ." });
            }

            var user = context.Users.FirstOrDefault(u => u.Email == loginUser.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Email không tồn tại." });
            }

            if (user.Password != loginUser.Password)
            {
                return Unauthorized(new { message = "Mật khẩu không đúng." });
            }

            return Ok(user);
        }



        [HttpPost("logout")]
        public IActionResult Logout(long userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return NotFound(new { message = "User không tồn tại." });
            }

            user.RememberToken = null; // Xóa token
            context.SaveChanges();

            return Ok(new { message = "Đăng xuất thành công." });
        }

        // *** Helpers ***
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
