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
    [Table("product_attributes")]
    public partial class Product_Attributes
    {
        [Key]
        public long Id { get; set; }
        public string? SKU { get; set; } // mã biến thể sản phẩm
        public long Product_Variant_Id { get; set; }
        public long? Regular_price { get; set; }
        public long? Sale_price { get; set; }
        public string? Image {  get; set; }
        public int? Stock { get; set; } // tồn kho tính theo tấm 
        public string? Status { get; set; } 
        public long? Color_Id { get; set; }
        public long? Size_Id { get; set; }
        [ForeignKey("Size_Id")]
        public virtual Size? Size { get; set; } 
        [ForeignKey("Color_Id")]
        public virtual Color? Color { get; set; }
        [ForeignKey("Product_Variant_Id")]
        public virtual Product_variants? Product_Variant { get; set; }
        [JsonIgnore]
        public virtual ICollection<P_attribute_discount> p_attribute_discount { get; set; } = new List<P_attribute_discount>();

    }
}
