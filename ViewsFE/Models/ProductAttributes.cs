using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ViewsFE.Models
{
    public partial class Product_Attributes
    {
        [Key]
        public long Id { get; set; }
        public long Product_Variant_Id { get; set; }
        public string? Image { get; set; }
        public string Sku { get; set; } // tồn kho tính theo tấm 
        public string? Status { get; set; }
        public long? Color_Id { get; set; }
        public long? Size_Id { get; set; }
        [ForeignKey("Size_Id")]
        public virtual Size? Size { get; set; }
        [ForeignKey("Color_Id")]
        public virtual Color? Color { get; set; }
        [ForeignKey("Product_Variant_Id")]
        public virtual Product_variants? Product_Variant { get; set; }

    }
}
