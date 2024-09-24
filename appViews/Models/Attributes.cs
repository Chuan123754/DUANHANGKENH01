using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace appViews.Models
{
    public partial class Attributes
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Title { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Slug { get; set; }
        public int Parent_Id { get; set; }
        public int Dept { get; set; }
        public string? Description { get; set; }
        public string? Feature_Image { get; set; }
        [StringLength(255)]
        public string? Color { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Template { get; set; }
        public int In_Order { get; set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product_variants> Product_Variants { get; set; } = new List<Product_variants>();
    }
}
