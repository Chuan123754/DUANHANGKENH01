namespace appAPI.Models.DTO
{

    public class ModelPostTag
    {
        public ProductModel objPost { get; set; }
        public List<long> lstTags { get; set; }
    }

    public class ProductModel
    {
        public long id { get; set; }
        public string Title { get; set; }
        public string slug { get; set; }  
        public string type { get; set; }
        public string status { get; set; }
        public long? AuthorId { get; set; }
        public bool Deleted { get; set; }
        public string? product_video { get; set; }
        public string? Short_description { get; set; }
        public string Description { get; set; }
        public string? Image_library { get; set; }
        public string Feature_image { get; set; }
    }

}
