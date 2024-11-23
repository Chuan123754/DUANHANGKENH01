namespace ClientViews.Models.DTO
{

    public class ModelPostTag
    {
        public ModelPostTag()
        {
            objPost = new ProductModel();
            lstTags = new List<TagDTO>();
            lstCategories = new List<CategoryDTO>();
        }
        public ProductModel objPost { get; set; }
        public List<TagDTO> lstTags { get; set; }
        public List<CategoryDTO> lstCategories { get; set; }
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
        public string StatusLabel => Product_Posts.STATUSES.ContainsKey(status) ? Product_Posts.STATUSES[status] : "Không xác định";

    }
    public class CategoryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class TagDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
