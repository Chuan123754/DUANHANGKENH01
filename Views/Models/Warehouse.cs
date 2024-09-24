using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Models
{
    [Table("warehouse")]
    public partial class Warehouse
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Address { get; set; }
        [StringLength(20)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có đúng 10 số.")]
        public string? PhoneNumber { get; set; }
        [StringLength(20)]
        public string? Status { get; set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
