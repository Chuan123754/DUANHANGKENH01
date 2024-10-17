namespace ViewsFE.DTO
{
    public class PostDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Status { get; set; }
        public long AuthorId { get; set; }
        public string? Type { get; set; }
        public DateTime? Post_date { get; set; }
        public long Created_by { get; set; }
        public long Updated_by { get; set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Content { get; set; }

        // Sử dụng List để lưu trữ ID của Category và Tag
        public List<long> SelectedCategoryId { get; set; } = new List<long>();
        public List<long> SelectedTagId { get; set; } = new List<long>();
    }

}
