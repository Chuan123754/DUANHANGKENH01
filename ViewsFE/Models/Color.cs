
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    public class Color
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Tên màu sắc bắt buộc.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Đường dẫn là bắt buộc.")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Mã màu là bắt buộc.")]
        public string? Color_code { get; set; }
        public string? Description { get; set; }
        public bool? Deleted { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Delete_at { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product_Attributes> Product_Attributes { get; set; } = new List<Product_Attributes>();
    }
}
