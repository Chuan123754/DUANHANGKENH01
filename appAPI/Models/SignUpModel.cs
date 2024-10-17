using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appAPI.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ ")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Mật khẩu phải có 6 kí tự bao gồm cả kí tự đặc biệt, chữ viết hoa, chữ viết thường và số ")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string ConfirmPassword { get; set; } = null!;
        [Required(ErrorMessage = "Chuyện quyền của người dùng")]
        public string Role { get; set; }
    }
}
