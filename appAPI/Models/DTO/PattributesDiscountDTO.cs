namespace appAPI.Models.DTO
{
    public class PattributesDiscountDTO
    {
        public long Id { get; set; }
        public long P_attribute_Id { get; set; }
        public long Discount_Id { get; set; }
        public decimal Old_price { get; set; }
        public decimal New_price { get; set; }
        public string Status { get; set; }
    }

}
