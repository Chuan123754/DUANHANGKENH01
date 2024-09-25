using System.ComponentModel.DataAnnotations;

namespace appAPI.Models
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
        public long Created_by { get; set; }
        public long Updated_by { get;set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }


    }
}
