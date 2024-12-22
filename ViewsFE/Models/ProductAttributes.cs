using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using appAPI.Models;

namespace ViewsFE.Models
{
    public partial class Product_Attributes
    {
        [Key]
        public long Id { get; set; }
        public string? SKU { get; set; } // mã biến thể sản phẩm
        public long? Regular_price { get; set; } // giá bán
        public long? Sale_price { get; set; } // giá giảm
        public string? Image { get; set; } // hình ảnh
        public int? Stock { get; set; } // tồn kho tính theo tấm 
        public string? Status { get; set; } // trạng thái ( xóa mềm )


        public long Post_Id { get; set; } // bài đăng vừa đăng
        public string? Description { get; set; } // mô tả
        public long? Textile_technology_id { get; set; }
        public long? Material_id { get; set; }
        public long? Style_id { get; set; }
        public long? Color_Id { get; set; }
        public long? Size_Id { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }


        [ForeignKey("Post_Id")]
        public virtual Product_Posts? Posts { get; set; }
        [ForeignKey("Style_id")]
        public virtual Style? Style { get; set; }
        [ForeignKey("Material_id")]
        public virtual Material? Material { get; set; }
        [ForeignKey("Textile_technology_id")]
        public virtual Textile_technology? Textile_Technology { get; set; }
        [ForeignKey("Size_Id")]
        public virtual Size? Size { get; set; }
        [ForeignKey("Color_Id")]
        public virtual Color? Color { get; set; }
        [JsonIgnore]
        public virtual ICollection<P_attribute_discount> ProductAttribute_Discount { get; set; } = new List<P_attribute_discount>();
        [JsonIgnore]
        public virtual ICollection<appAPI.Models.ProductAttributes_wishlist> ProductAttributes_Wishlists { get; set; } = new List<appAPI.Models.ProductAttributes_wishlist>();

    }
}
