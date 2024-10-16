using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewsFE.Models
{
    public class P_variants_discount
    {
        [Key]
        public long Id { get; set; }
        public long P_variants_Id { get; set; }    
        public long Discount_Id { get; set; }
        public decimal Old_price { get; set; }
        public decimal New_price { get; set;}
        public string Status { get; set; }
        [ForeignKey("P_variants_Id")]
        public virtual Product_variants ProductVariant { get; set; }
        [ForeignKey("Discount_Id")]
        public virtual Discount Discount { get; set; }
    }
}
