namespace appAPI.DTO
{
    public class OrderTrackingDTO
    {
        public List<OrderTrackingItem> OrderTrackings { get; set; }
        public long OrderId { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public string Address { get; set; }
        public string? Status { get; set; } 
        public string? Note { get; set; } 
        public DateTime? Created_at { get; set; }
        public List<ProductItem> Products { get; set; }
        public string TypePayment { get; set; }      // Loại thanh toán
        public decimal TotalPrincipal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FeeShipping { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal TotalVoucher { get; set; }
        public long Created_by { get; set; }

    }

    public class OrderTrackingItem
    {
        public long Id { get; set; }            // ID của order_trackings
        public string Status { get; set; }       // Trạng thái đơn hàng
        public string? CreateBy { get; set; }       

        public string PreviousStatus { get; set; }
        public string Note { get; set; }         // Ghi chú
        public decimal TotalMoney { get; set; }  // Tổng tiền
        public DateTime CreatedAt { get; set; }  // Ngày tạo
    }


    public class ProductItem
    {
        public string SKU { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalDiscount { get; set; }
        public string? Image { get; set; }

    }
}
