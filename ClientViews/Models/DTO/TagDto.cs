namespace ClientViews.Models.DTO
{
    public class TagDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
