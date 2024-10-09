
using System.ComponentModel.DataAnnotations;

namespace appAPI.Models
{
    public class Material
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Delete_at { get; set; }
        public virtual ICollection<Product_Posts> Product_Posts { get; set; } = new List<Product_Posts>();
    }
}
