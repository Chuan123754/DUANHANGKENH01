using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appAPI.Models
{
    public class OrderVouchers
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; } 
        public long VoucherId { get; set; } 
        public DateTime AppliedAt { get; set; } 
        [ForeignKey("OrderId")]
        public virtual Orders? Orders { get; set; }
        [ForeignKey("VoucherId")]
        public virtual Vouchers? Vouchers { get; set; }
    }
}
