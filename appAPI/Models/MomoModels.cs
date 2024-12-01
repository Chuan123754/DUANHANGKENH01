using System.ComponentModel;

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
        public string PartnerCode { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string RequestType { get; set; }
        public string NotifyUrl { get; set; } // URL API nhận thông báo từ MoMo
        public string ReturnUrl { get; set; } // URL chuyển hướng người dùng sau thanh toán
        public string MomoApiUrl { get; set; }
        public string ConnectionString { get; set; } // Thêm chuỗi kết nối CSDL
    }


    public class MomoQueryPaymentResponseModel
    {
        public string OrderId { get; set; }
        public string RequestId { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string PayType { get; set; }
        public string TransId { get; set; }
        public long Amount { get; set; }
    }

    public class MomoNotificationModel
    {
        public string PartnerCode { get; set; }
        public string AccessKey { get; set; }
        public string OrderId { get; set; }
        public string RequestId { get; set; }
        public long Amount { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public long TransId { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string PayType { get; set; }
        public string ResponseTime { get; set; }
        public string Signature { get; set; }
    }

}
