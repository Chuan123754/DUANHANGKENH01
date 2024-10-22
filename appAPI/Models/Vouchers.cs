using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    public class Vouchers
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Mã voucher là bắt buộc.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Mã voucher phải có độ dài từ 5 đến 20 ký tự.")]
        public string? Code { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự.")]
        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là một số dương.")]
        public string? Quantity { get; set; }

        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải nằm trong khoảng từ 0 đến 100.")]
        public string? Percent { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá trị giảm tối đa phải là một số dương.")]
        public string? MaxDiscountValue { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Điều kiện phải là một số dương.")]
        public string? Condition { get; set; }
        public DateTime Create_at { get; set; }

        public DateTime Update_at { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu là bắt buộc.")]
        public DateTime Start_time { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc là bắt buộc.")]
        [DateGreaterThan("Start_time", ErrorMessage = "Thời gian kết thúc phải sau thời gian bắt đầu.")]
        public DateTime End_time { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        public string? Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order_Vouchers> OrderVouchers { get; set; } = new List<Order_Vouchers>();
        [JsonIgnore]
        public virtual ICollection<UserVouchers> UserVouchers { get; set; } = new List<UserVouchers>();

    }

    // Custom validation attribute để kiểm tra thời gian kết thúc phải lớn hơn thời gian bắt đầu
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateGreaterThanAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime)value;

            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (startDateProperty == null)
                return new ValidationResult($"Không tìm thấy thuộc tính {_startDatePropertyName}");

            var startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate <= startDate)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
