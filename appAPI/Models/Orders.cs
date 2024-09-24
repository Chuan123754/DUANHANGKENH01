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
    [Index("Address", Name = "orders_address_fulltext")]
    [Index("Approved_at", Name = "orders_approved_at_index")]
    [Index("Code", Name = "orders_code_index")]
    [Index("Name", Name = "orders_name_index")]
    [Index("Phone_number", Name = "orders_phone_number_index")]
    [Index("Status", Name = "orders_status_index")]
    [Index("User_id", Name = "orders_user_id_index")]
    [Index("Warehouse_id", Name = "orders_warehouse_id_index")]

    public partial class Orders
    {
        [Key]
        public long Id { get; set; }
        public long? CartId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Code { get; set; }
        public long? Warehouse_id { get; set; }
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
 
        [ForeignKey("CartId")]
        public virtual Carts? Cart { get; set; }
        [InverseProperty("Orders")]
        [JsonIgnore]
        public virtual ICollection<Order_details> Order_details { get; set; } = new List<Order_details>();
        [InverseProperty("Orders")]
        [JsonIgnore]
        public virtual ICollection<order_trackings> Order_trackings { get; set; } = new List<order_trackings>();
        [JsonIgnore]
        public virtual ICollection<OrderVouchers> OrderVouchers { get; set; } = new List<OrderVouchers>();
    }
}

