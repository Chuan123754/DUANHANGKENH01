using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    public class Discount
    {
        [Key]
        public long Id { get; set; }
        public string? Code { get; set; }    
        public string? Name { get; set; }    
        public string? Type_of_promotion { get; set; }
        [Required]
        public decimal Discount_value { get; set; } // Giá trị giảm giá (vd: 20%, 100,000 VND)
        public bool? IsGlobal { get; set; } = false; // áp dụng cho tất cả sản phẩm
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set;}
        public string? Status { get; set; }
        public long? Created_by { get; set; }
        public long? Updated_by { get; set; }
        public DateTime? Create_at { get; set; }
        public DateTime? Update_at { get; set;}
        [JsonIgnore]
        public virtual ICollection<P_attribute_discount> PAttributeDiscounts { get; set; } = new List<P_attribute_discount>();
    }
}
