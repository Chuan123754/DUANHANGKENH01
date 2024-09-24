using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace appAPI.Models
{
    public class Vouchers
    {
        public long Id { get; set; }    
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Quantity { get; set; }
        public string? Percent { get; set; }
        public string? MaxDiscountValue { get; set; } // giá trị giảm tối đa
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderVouchers> OrderVouchers { get; set; } = new List<OrderVouchers>();
    }
}
