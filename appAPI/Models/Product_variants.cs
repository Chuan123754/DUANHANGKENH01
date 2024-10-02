using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    [Table("product_variants")]
    public partial class Product_variants
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        [StringLength(255)]
        public string? Image { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Sku { get; set; }
        [StringLength(20)]
        public string? Status { get; set; }
        public long? Regular_price { get; set; }
        public long? Sale_price { get; set; }
        public int Stock_quantity { get; set; }
        public string? Description { get; set; } 
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        public virtual ICollection<Product_attributes> Product_attributes { get; set; } = new List<Product_attributes>();
        [ForeignKey("Post_Id")]
        [JsonIgnore]
        public virtual Posts? Posts { get; set; }
        public virtual ICollection<P_variants_discount> p_variants_discount { get; set; } = new List<P_variants_discount>();
        public virtual ICollection<Product_variants_wishlist> Product_Variants_Wishlists { get; set; } = new List<Product_variants_wishlist>();
    }
}
