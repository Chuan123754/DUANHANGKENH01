using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewsFE.Models
{
    [Table("order_details")]
    public partial class Order_details
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long Product_Attribute_Id { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; } // giá bán ban đầu
        public decimal? TotalDiscount { get; set; } // giá bán sau giảm
        [ForeignKey("OrderId")]
        public virtual Orders? Orders { get; set; }
        [ForeignKey("Product_Attribute_Id")]
        public virtual Product_Attributes? ProductAttributes { get; set; }
        public virtual Products_Returned ProductsReturned { get; set; } = new Products_Returned();

        [NotMapped]
        public bool IsSelected { get; set; } = false; //tích

        [NotMapped]
        public int ReturnQuantity { get; set; } = 0;// Số lượng muốn đổi


    }
}
