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
    [Table("Cartdetails")]
    public partial class Cart_details
    {
        [Key]
        public long Id { get; set; }
        public long Product_id { get; set; }
        public long Cart_id { get; set; }
        public int Quantity { get; set; }
        [StringLength(20)]
        public string? Status {  get; set; }
        [ForeignKey("Cart_id")]
        public virtual Carts? Carts { get; set; }
        [ForeignKey("Product_id")]
        public virtual Product_Attributes? Product_Attributes { get; set; }
    }
}
