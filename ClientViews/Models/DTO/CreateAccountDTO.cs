using System.ComponentModel.DataAnnotations;

namespace ClientViews.Models.DTO
{
    public class CreateAccountDTO
    {
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ là bắt buộc.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vai trò là bắt buộc.")]
        public string SelectedRole { get; set; } = string.Empty;
    }
}
