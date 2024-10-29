using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewsFE.Models
{
    [Table("order_details")]
    public partial class Order_details
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductVariants_Id { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 4)]
        public decimal RegularPrice { get; set; }
        [Precision(18, 4)]
        public decimal SalePrice { get; set; }
        [Precision(18, 4)]
        public decimal Price { get; set; }
        public int RemainingStockQuantity { get; set; }
        [StringLength(255)]
        public string? ProductTitle { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public virtual Orders? Orders { get; set; }
        [ForeignKey("ProductVariants_Id")]
        [JsonIgnore]
        public virtual Product_variants? ProductVariants { get; set; }
    }
}
