namespace ViewsFE.DTO
{
    public class ProductDTO
    {
        public string ProductName { get; set; }  // Tên sản phẩm
        public string Image { get; set; }        // Hình ảnh
        public string Status { get; set; }       // Trạng thái
        public string Description { get; set; }  // Mô tả
        public string TextileTechnology { get; set; }  // Công nghệ dệt
        public string Style { get; set; }        // Phong cách
        public string Material { get; set; }     // Chất liệu
        public string Size { get; set; }         // Size
        public string Color { get; set; }        // Màu
        public int? Stock { get; set; }          // Số lượng
        public decimal? RegularPrice { get; set; } // Giá tiền
    }
}
