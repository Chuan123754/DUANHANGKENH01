using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Views.Models
{
    [Table("product_variants")]
    [Index("Attribute_Id", Name = "IX_product_variants_attribute_id")]
    [Index("Product_Id", Name = "IX_product_variants_product_id")]
    [Index("Sku", Name = "UK_product_variants_sku", IsUnique = true)]
    public partial class Product_variants
    {
        [Key]
        public long Id { get; set; }
        public long Product_Id { get; set; }
        public long Attribute_Id { get; set; }
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
        [ForeignKey("Attribute_Id")]
        [JsonIgnore]
        public virtual Attributes? Attribute { get; set; }
        [ForeignKey("Product_Id")]
        [JsonIgnore]
        public virtual Products? Product { get; set; }
    }
}
