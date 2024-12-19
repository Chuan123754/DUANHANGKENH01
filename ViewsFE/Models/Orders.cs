using appAPI.Models;
using Microsoft.EntityFrameworkCore;
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
    [Table("orders")]

    public partial class Orders
    {
        [Key]
        public long Id { get; set; }
        public string? CreatedByAdminId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalPrincipal { get; set; } // tổng tiền hàng 
        public decimal? FeeShipping { get; set; }
        public decimal? Totalmoney { get; set; }
        public long? User_id { get; set; } // khách hàng ( hóa đơn treo có thể chưa thêm khách hàng )
        [StringLength(20)]
        public string? Status { get; set; }
        public string? Note { get; set; }
        public string? TypePayment { get; set; }

        public DateTime? Approved_at { get; set; }
        public DateTime? Created_at { get; set; } = DateTime.Now;
        public DateTime? Update_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order_details> Order_details { get; set; } = new List<Order_details>();
        [JsonIgnore]
        public virtual ICollection<order_trackings> Order_trackings { get; set; } = new List<order_trackings>();
        [JsonIgnore]
        public virtual ICollection<Order_Vouchers> OrderVouchers { get; set; } = new List<Order_Vouchers>();
        [ForeignKey("CreatedByAdminId")]
        public virtual Account? Admin { get; set; }
        [ForeignKey("User_id")]
        public virtual Users? Users { get; set; }
        public long? Address_Id { get; set; }
        [ForeignKey("Address_Id")]
        public virtual Address? Address { get; set; }
        public long? Payment_Id { get; set; }
        [ForeignKey("Payment_Id")]
        [JsonIgnore]
        public virtual Payment? Payment { get; set; }

        // Define constant values for statuses
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

