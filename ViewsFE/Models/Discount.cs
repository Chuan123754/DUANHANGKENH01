using System.ComponentModel.DataAnnotations;

namespace ViewsFE.Models
{
    public class Discount
    {
        [Key]
        public long Id { get; set; }
        public string Code { get; set; }    
        public string Name { get; set; }    
        public string Type_of_promotion { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set;}
        public string Status { get; set; }
        public long Created_by { get; set; }
        public long Updated_by { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set;}
        public virtual ICollection<P_variants_discount> ProductVariantsDiscounts { get; set; } = new List<P_variants_discount>();

    }
}
