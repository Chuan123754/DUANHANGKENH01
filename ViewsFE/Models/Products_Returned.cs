using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ViewsFE.Models
{
    [Table("products_returned")]
    public class Products_Returned
    {
        public long Id { get; set; }
        [Required]
        public long OrderDetailId { get; set; }

        [Required]
        [StringLength(255)]
        public string ReturnReason { get; set; } = string.Empty; // Lý do trả hàng
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string Condition { get; set; } = string.Empty; // Tình trạng sản phẩm (ví dụ: Hỏng, Sửa được...)

        [Required]
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow; // Ngày trả hàng

        public string? Notes { get; set; }
        public bool? IsReturn { get; set; }
        public DateTime? Created_at { get; set; } = DateTime.Now;
        public long? Created_by { get; set; }
        public DateTime? Update_at { get; set; }

        [ForeignKey("OrderDetailId")]
        [JsonIgnore]
        public virtual Order_details? OrderDetails { get; set; }
    }
}
