using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace appAPI.Models
{
    [Table("orders")]

    public partial class Orders
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Code { get; set; }
        public long? User_id { get; set; }
        public decimal Total { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? Phone_number { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Address { get; set; }
        [StringLength(20)]
        public string? Status { get; set; }
        public string? Note { get; set; }
        public DateTime? Approved_at { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order_details> Order_details { get; set; } = new List<Order_details>();
        [JsonIgnore]
        public virtual ICollection<order_trackings> Order_trackings { get; set; } = new List<order_trackings>();
        [JsonIgnore]
        public virtual ICollection<Order_Vouchers> OrderVouchers { get; set; } = new List<Order_Vouchers>();
        [ForeignKey("User_id")]
        [JsonIgnore]

        public virtual Users? Users { get; set; }
    }
}

