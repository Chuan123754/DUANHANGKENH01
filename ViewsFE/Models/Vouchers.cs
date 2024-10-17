using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewsFE.Models;

namespace ViewsFE.Models
{
    public class Vouchers
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Quantity { get; set; }
        public string? Percent { get; set; }
        public string? MaxDiscountValue { get; set; } // giá trị giảm tối đa
        public string? Condition { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order_Vouchers> OrderVouchers { get; set; } = new List<Order_Vouchers>();
        [JsonIgnore]
        public virtual ICollection<UserVouchers> UserVouchers { get; set; }

    }
}
