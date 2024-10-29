namespace appAPI.Models.DTO
{
    public class PVariantsDiscountDTO
    {
        public long P_variants_Id { get; set; }
        public long Discount_Id { get; set; }
        public decimal Old_price { get; set; }
        public decimal New_price { get; set; }
        public string Status { get; set; }
    }

}
