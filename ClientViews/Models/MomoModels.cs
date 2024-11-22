namespace appAPI.Models
{
    public class MomoCreatePaymentResponseModel
    {
        public string RequestId { get; set; }
        public int ErrorCode { get; set; }
        public string OrderId { get; set; }
        public string Message { get; set; }
        public string PayUrl { get; set; }
        public string Signature { get; set; }
    }

    public class OrderInfoModel
    {
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
    }

    public class MomoOptionModel
    {
        public string MomoApiUrl { get; set; }
        public string SecretKey { get; set; }
        public string AccessKey { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string PartnerCode { get; set; }
        public string RequestType { get; set; }
    }

}
