using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
  
    }
}
