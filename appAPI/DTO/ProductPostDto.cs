using System.ComponentModel.DataAnnotations;

namespace ViewsFE.DTO
{
    public class ProductPostDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255, ErrorMessage ="Tiêu đề phải nhỏ hơn 255 ký tự")]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }

        public string? Status { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? ImageLibrary { get; set; }

        public List<string> FeatureImage { get; set; } = new List<string>();

        public DateTime? PostDate { get; set; }

        public long? AuthorId { get; set; }

        public List<long> Tags { get; set; } = new List<long>();

        public List<long> Categories { get; set; } = new List<long>();
    }

}
