using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


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
        public IActionResult Post(Users user)
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
            item.OTPCheck = user.OTPCheck;
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

            // Xóa giỏ hàng liên kết với user
            var carts = context.Carts.Where(c => c.UserId == id).ToList();
            context.Carts.RemoveRange(carts);

            // Xóa wishlist liên kết với user
            var wishlists = context.Wishlist.Where(w => w.User_id == id).ToList();
            context.Wishlist.RemoveRange(wishlists);

            // Xóa user
            context.Users.Remove(delete);
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

        [HttpPost("register-sync")]
        public IActionResult RegisterSync([FromBody] Users user)
        {
            if (string.IsNullOrEmpty(user.Phone))
            {
                return BadRequest(new { message = "Số điện thoại không được để trống." });
            }

            // Kiểm tra nếu số điện thoại đã tồn tại
            var existingUser = context.Users.FirstOrDefault(u => u.Phone == user.Phone);

            if (existingUser != null)
            {
                if (string.IsNullOrEmpty(existingUser.Password))
                {
                    // Nếu mật khẩu là null, cập nhật mật khẩu mới
                    existingUser.Password = user.Password;
                    existingUser.Email = user.Email;
                    existingUser.Updated_at = DateTime.Now;
                    context.SaveChanges();

                    return Ok(new { message = "Cập nhật tài khoản thành công.", existingUser.Id});
                }
                else
                {
                    return BadRequest(new { message = "Số điện thoại đã tồn tại và tài khoản đã có mật khẩu." });
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    // Nếu số điện thoại không tồn tại và mật khẩu được cung cấp, tạo mới bản ghi
                    context.Users.Add(user);
                    context.SaveChanges();

                    return Ok(new { message = "Tài khoản mới đã được tạo thành công.", user });
                }
                else
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ. Vui lòng cung cấp mật khẩu." });
                }
            }
        }




        [HttpGet("GetByPhoneNumber")]
        public IActionResult GetByPhoneNumber(string phoneNumber)
        {
            var user = context.Users.FirstOrDefault(u => u.Phone == phoneNumber);
            if (user == null)
            {
                return NotFound(new { message = "Số điện thoại không tồn tại trong hệ thống." });
            }
            return Ok(user);
        }


        [HttpGet("GetUserByPhone")]
        public IActionResult GetUserByPhone(string phone)
        {
            var user = context.Users.FirstOrDefault(u => u.Phone == phone);
            if (user != null && !string.IsNullOrEmpty(user.Password))
            {
                // Nếu tìm thấy và có mật khẩu
                return Ok(new { exists = true});
            }

            // Không tìm thấy hoặc không có mật khẩu
            return Ok(new { exists = false});
        }



        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(long userId, [FromBody] string newPassword)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại." });
            }

            // Mã hóa mật khẩu mới bằng PasswordHasher
            var passwordHasher = new PasswordHasher<object>();
            user.Password = passwordHasher.HashPassword(null, newPassword);

            user.Updated_at = DateTime.Now;
            context.SaveChanges();

            return Ok(new { message = "Mật khẩu đã được cập nhật thành công." });
        }

        [HttpGet("check-cookie")]
        public IActionResult CheckCookie()
        {
            if (Request.Cookies.TryGetValue("RememberToken", out var token))
            {
                return Ok(new { message = "Cookie tồn tại", token });
            }
            return NotFound(new { message = "Cookie không tồn tại" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(new { message = "Dữ liệu đầu vào không hợp lệ." });
            }

            var user = context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Email không tồn tại." });
            }

            var passwordHasher = new PasswordHasher<object>();
            var verificationResult = passwordHasher.VerifyHashedPassword(null, user.Password, loginRequest.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Mật khẩu không đúng." });
            }

            return Ok(user);
        }

        [HttpGet("auto-login")]
        public IActionResult AutoLogin()
        {
            if (Request.Cookies.TryGetValue("RememberToken", out var token))
            {
                var user = context.Users.FirstOrDefault(u => u.RememberToken == token);
                if (user != null)
                {
                    return Ok(user); // Trả về thông tin người dùng
                }
            }
            return Unauthorized(new { message = "Không tìm thấy token hợp lệ" });
        }

        [HttpGet("test-cookie")]
        public IActionResult TestCookie()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = false
            };
            Response.Cookies.Append("TestCookie", "TestValue", cookieOptions);
            return Ok(new { message = "Cookie set successfully" });
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


        [HttpGet("GetTotalUsersByDay")]
        public async Task<IActionResult> GetTotalUsersByDay()
        {
            try
            {
                var today = DateTime.Today; // Ngày hiện tại
                var totalUsers = await context.Users
                    .Where(u => u.Created_at.HasValue && u.Created_at.Value.Date == today)
                    .CountAsync();
                return Ok(totalUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("GetTotalUsersByMonth")]
        public async Task<IActionResult> GetTotalUsersByMonth()
        {
            try
            {
                var today = DateTime.Today;
                var startOfMonth = new DateTime(today.Year, today.Month, 1); // Đầu tháng
                var totalUsers = await context.Users
                    .Where(u => u.Created_at.HasValue && u.Created_at.Value >= startOfMonth)
                    .CountAsync();
                return Ok(totalUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Lấy tổng khách hàng theo năm
        [HttpGet("GetTotalUsersByYear")]
        public async Task<IActionResult> GetTotalUsersByYear()
        {
            try
            {
                var today = DateTime.Today;
                var startOfYear = new DateTime(today.Year, 1, 1); // Đầu năm
                var totalUsers = await context.Users
                    .Where(u => u.Created_at.HasValue && u.Created_at.Value >= startOfYear)
                    .CountAsync();
                return Ok( totalUsers );
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Lấy tổng tất cả khách hàng
        [HttpGet("GetTotalUsers")]
        public async Task<IActionResult> GetTotalUsers()
        {
            try
            {
                var totalUsers = await context.Users.CountAsync();
                return Ok( totalUsers );
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        // Top 5 khách hàng mua nhiều hóa đơn nhất (Tổng quan)
        [HttpGet("GetTop5Customers")]
        public async Task<IActionResult> GetTop5Customers()
        {
            try
            {
                var topCustomers = await context.Users
                    .Include(u => u.Orders)  // Bao gồm các đơn hàng của khách hàng
                    .Select(u => new
                    {
                        u.Id,
                        u.Name,
                        TotalOrders = u.Orders.Count,  // Đếm số đơn hàng của khách hàng
                        TotalMoneySpent = u.Orders
                            .Sum(o => o.Totalmoney ?? 0)  // Tính tổng tiền đã chi (Totalmoney), xử lý trường hợp null
                    })
                    .OrderByDescending(u => u.TotalOrders)  // Sắp xếp theo số đơn hàng
                    .Take(5)  // Lấy 5 khách hàng có số lượng đơn hàng cao nhất
                    .ToListAsync();

                return Ok(topCustomers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        // Top 5 khách hàng mua nhiều hóa đơn nhất trong tuần
        [HttpGet("GetTop5CustomersWeekly")]
        public async Task<IActionResult> GetTop5CustomersWeekly()
        {
            try
            {
                var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

                var topCustomers = await context.Users
                    .Include(u => u.Orders)  // Bao gồm các đơn hàng của khách hàng
                    .Select(u => new
                    {
                        u.Id,
                        u.Name,
                        TotalOrders = u.Orders.Count(o => o.Created_at >= startOfWeek),  // Đếm số đơn hàng trong tuần
                        TotalMoneySpent = u.Orders
                            .Where(o => o.Created_at >= startOfWeek)  // Chỉ tính đơn hàng trong tuần
                            .Sum(o => o.Totalmoney)  // Tính tổng tiền đã chi (Totalmoney)
                    })
                    .OrderByDescending(u => u.TotalOrders)  // Sắp xếp theo số đơn hàng
                    .Take(5)  // Lấy 5 khách hàng có số lượng đơn hàng cao nhất
                    .ToListAsync();

                return Ok(topCustomers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        // Top 5 khách hàng mua nhiều hóa đơn nhất trong tháng
        [HttpGet("GetTop5CustomersMonthly")]
        public async Task<IActionResult> GetTop5CustomersMonthly()
        {
            try
            {
                var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1); // Lấy ngày đầu tháng hiện tại

                var topCustomers = await context.Users
                    .Include(u => u.Orders)
                    .Select(u => new
                    {
                        u.Id,
                        u.Name,
                        TotalOrders = u.Orders.Count(o => o.Created_at >= startOfMonth), // Đếm số đơn hàng trong tháng này
                        TotalMoneySpent = u.Orders
                            .Where(o => o.Created_at >= startOfMonth)  // Lọc các đơn hàng trong tháng này
                            .Sum(o => o.Totalmoney ?? 0)  // Tính tổng tiền đã chi, xử lý trường hợp null
                    })
                    .OrderByDescending(u => u.TotalOrders)  // Sắp xếp theo số đơn hàng
                    .Take(5)  // Lấy 5 khách hàng có số lượng đơn hàng cao nhất
                    .ToListAsync();

                return Ok(topCustomers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        // Top 5 khách hàng mua nhiều hóa đơn nhất trong năm
        [HttpGet("GetTop5CustomersYearly")]
        public async Task<IActionResult> GetTop5CustomersYearly()
        {
            try
            {
                var startOfYear = new DateTime(DateTime.UtcNow.Year, 1, 1); // Lấy ngày đầu năm hiện tại

                var topCustomers = await context.Users
                    .Include(u => u.Orders)
                    .Select(u => new
                    {
                        u.Id,
                        u.Name,
                        TotalOrders = u.Orders.Count(o => o.Created_at >= startOfYear), // Đếm số đơn hàng trong năm này
                        TotalMoneySpent = u.Orders
                            .Where(o => o.Created_at >= startOfYear)  // Lọc các đơn hàng trong năm này
                            .Sum(o => o.Totalmoney ?? 0)  // Tính tổng tiền đã chi, xử lý trường hợp null
                    })
                    .OrderByDescending(u => u.TotalOrders)  // Sắp xếp theo số đơn hàng
                    .Take(5)  // Lấy 5 khách hàng có số lượng đơn hàng cao nhất
                    .ToListAsync();

                return Ok(topCustomers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

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

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

    }
}
