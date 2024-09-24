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
    [Table("seo")]
    public partial class Seo
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Model_type { get; set; }
        public long Post_Id { get; set; }
        [StringLength(255)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
