namespace ViewsFE.Models.DTO
{
    public class ProductPostDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Status { get; set; }
        public long? AuthorId { get; set; }
        public bool? Deleted { get; set; }
        public string? Type { get; set; }
        public string? ProductVideo { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ImageLibrary { get; set; }
        public string? FeatureImage { get; set; }
        public string? CategoryName { get; set; }
        public string? TagName { get; set; }
        public DateTime? PostDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<PostTagDto>? Tags { get; set; } = new List<PostTagDto>();
        public List<Product_Attributes>? Product_Attributes { get; set; } = new List<Product_Attributes>();
        public List<Post_categories>? PostCategories { get; set; } = new List<Post_categories>();

    }
}
