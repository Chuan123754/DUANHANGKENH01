using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Models
{
    [Table("banners")]
    public partial class Banner
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        [StringLength(20)]
        public string? Type { get; set; }
        [StringLength(20)]
        public string? Status { get; set; }
        public string? Meta_data { get; set; }
        public long Created_by { get; set; }
        public long Updated_by { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get;set; }
    }
}
