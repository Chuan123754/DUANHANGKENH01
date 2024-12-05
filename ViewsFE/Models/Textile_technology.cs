using System.ComponentModel.DataAnnotations;

namespace ViewsFE.Models
{
    public class Textile_technology
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Đường dẫn là bắt buộc.")]
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public bool? Deleted { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Delete_at { get; set; }
        public virtual ICollection<Product_variants> Product_Variants { get; set; } = new List<Product_variants>();
    }
}
