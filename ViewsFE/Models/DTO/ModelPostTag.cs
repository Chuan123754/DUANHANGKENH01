namespace ViewsFE.Models.DTO
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
        public string shortDes { get; set; }
        public string longDes { get; set; }
        public string type { get; set; }
        public string status { get; set; }
    }

}
