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
    [Table("order_trackings")]
    public partial class order_trackings
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string? Note { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set;}
        public string? Created_by { get; set; }
        [StringLength(100)]
        public string? Status { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("Order_trackings")]
        [JsonIgnore]
        public virtual Orders? Orders { get; set; }

        public const string STATUS_COMPLETED = "completed";
        public const string STATUS_PAID = "paid";
        public const string STATUS_CONFIREMD = "confirmed";
        public const string STATUS_RECEIVED = "received";
        public const string STATUS_CANCEL = "cancel";
        public const string STATUS_WAIT = "wait";
        public const string STATUS_HANGINGINVOICE = "hanginginvoice";
        public const string STATUS_PANDING = "pending";
        public const string STATUS_PREPAREDGOODS = "preparedgoods";
        public const string STATUS_SHIPING = "shipping";
        public const string STATUS_DELIVEREDSUCCESSFULLY = "deliveredsuccessfully";
        public const string STATUS_DELIVERYFAILED = "faileddelivery";
        public const string STATUS_EXCHANGEGOODS = "exchangegoods";
        public const string STATUS_RETURNS = "returns";
        public const string STATUS_PENDING = "Pending";
        // Dictionary to hold status labels
        public static readonly Dictionary<string, string> STATUSES = new Dictionary<string, string>
        {
             { STATUS_COMPLETED, "Hoàn tất đơn" },
             { STATUS_PAID, "Đã thanh toán" },
             { STATUS_CONFIREMD, "Đã xác nhận" },
             { STATUS_RECEIVED, "Đã lấy hàng" },
             { STATUS_CANCEL, "Đơn huỷ" },
             { STATUS_WAIT, "Chờ xác nhận" },
             { STATUS_HANGINGINVOICE, "Hoá đơn treo" },
             { STATUS_PANDING, "Pending" },
             { STATUS_PREPAREDGOODS, "Đã chuẩn bị hàng" },
             { STATUS_SHIPING, "Đang vận chuyển" },
             { STATUS_DELIVEREDSUCCESSFULLY, "Giao hàng thành công" },
             { STATUS_DELIVERYFAILED, "Giao hàng thất bại" },
             { STATUS_EXCHANGEGOODS, "Đổi hàng" },
             { STATUS_RETURNS, "Trả hàng" },
             { STATUS_PENDING, "Pending" }

        };

        // Dictionary to hold status classes (for styling purposes)
        public static readonly Dictionary<string, string> STATUS_CLASSES = new Dictionary<string, string>
        {
             { STATUS_COMPLETED, "text-success" },
             { STATUS_PAID, "text-primary" },
             { STATUS_CONFIREMD, "text-muted" },
             { STATUS_RECEIVED, "text-secondary" },
             { STATUS_CANCEL, "text-danger" },
             { STATUS_WAIT, "text-warning" },
             { STATUS_HANGINGINVOICE, "text-info" },
             { STATUS_PANDING, "text-dark" },
             { STATUS_PREPAREDGOODS, "text-info" },
             { STATUS_SHIPING, "text-primary" },
             { STATUS_DELIVEREDSUCCESSFULLY, "text-success" },
             { STATUS_DELIVERYFAILED, "text-danger" },
             { STATUS_EXCHANGEGOODS, "text-warning" },
             { STATUS_RETURNS, "text-secondary" }
        };

        // Get the label for the current status
        public string StatusLabel => STATUSES.ContainsKey(Status) ? STATUSES[Status] : string.Empty;

        // Get the CSS class for the current status
        public string StatusClass => STATUS_CLASSES.ContainsKey(Status) ? STATUS_CLASSES[Status] : string.Empty;

    }
}
