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
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Quantity { get; set; }
        public string? Percent { get; set; }
        public string? MaxDiscountValue { get; set; }
        public string? Condition { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public string? Status { get; set; }

    }
        //[Key]
        //public long Id { get; set; }
        //public string? Code { get; set; }
        //public string? Description { get; set; }
        //public int? Quantity { get; set; }
        //public string? Percent { get; set; }
        //public decimal? MaxDiscountValue { get; set; } // Giá trị giảm tối đa (nếu là %)
        //public string? Condition { get; set; } // Điều kiện áp dụng voucher
        //public DateTime? Create_at { get; set; }
        //public DateTime? Update_at { get; set; }
        //public DateTime? Start_time { get; set; }
        //public DateTime? End_time { get; set; }
        //public string? Status { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<UserVouchers> UserVouchers { get; set; } = new List<UserVouchers>();

        //[JsonIgnore]
        //public virtual ICollection<Order_Vouchers> OrderVouchers { get; set; } = new List<Order_Vouchers>(); // Quan hệ với OrderVoucher
}
