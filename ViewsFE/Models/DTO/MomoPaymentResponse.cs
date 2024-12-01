namespace ViewsFE.Models.DTO
{
    public class MomoPaymentResponse
    {
        public string PayUrl { get; set; }
        public string QrCodeBase64 { get; set; }
        public string ResultCode { get; set; }
    }
}
