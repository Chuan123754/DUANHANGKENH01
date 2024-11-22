using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientViews.Models
{
    public class P_attribute_discount
    {
        [Key]
        public long Id { get; set; }
        public long P_attribute_Id { get; set; }
        public long Discount_Id { get; set; }
        public decimal Old_price { get; set; }
        public decimal New_price { get; set; }
        public string Status { get; set; }
        [ForeignKey("P_attribute_Id")]
        [JsonIgnore]
        public virtual Product_Attributes ProductAttributes { get; set; }
        [ForeignKey("Discount_Id")]
        [JsonIgnore]
        public virtual Discount Discount { get; set; }
    }
}
