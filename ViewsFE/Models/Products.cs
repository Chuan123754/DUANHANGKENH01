using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("post_products")]
    [Index("Post_Id", Name = "IX_post_products_post_id")]
    [Index("Sku", Name = "UK_post_products_sku", IsUnique = true)]
    public partial class Products
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public long Designer_Id { get;set; }
        public string? Sku { get; set; }
        [StringLength(50)]
        public string? Status { get; set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [ForeignKey("Post_Id")]
        [JsonIgnore]
        public virtual Posts? Posts { get; set; }
        [JsonIgnore]

        public virtual ICollection<Product_variants> Product_variants { get; set; } = new List<Product_variants>();
        [JsonIgnore]

        public virtual ICollection<Wishlist> Wishlist { get; set; } = new List<Wishlist>();
        [JsonIgnore]
        public virtual ICollection<Designer> Designer { get; set; } 
    }
}
