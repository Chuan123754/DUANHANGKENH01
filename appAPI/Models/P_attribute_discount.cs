using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    [Table("ProductAttributes_Discount")]
    public class P_attribute_discount
    {
        [Key]
        public long Id { get; set; }
        public long? P_attribute_Id { get; set; }    
        public long? Discount_Id { get; set; }
        public string? Status { get; set; }
        public DateTime? AppliedDate { get; set; }
        [ForeignKey("P_attribute_Id")]
        public virtual Product_Attributes? ProductAttributes { get; set; } 
        [ForeignKey("Discount_Id")]
        public virtual Discount? Discount { get; set; } 
    }
}
