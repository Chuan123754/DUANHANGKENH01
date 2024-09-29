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
        public long Created_by { get; set; }
        [StringLength(20)]
        public string? Status { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("Order_trackings")]
        [JsonIgnore]
        public virtual Orders? Orders { get; set; }

    }
}
