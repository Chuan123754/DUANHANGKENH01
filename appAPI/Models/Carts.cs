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
    [Table("carts")]
    public partial class Carts
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cart_details> Cart_Details { get; set; } = new List<Cart_details>();
        [ForeignKey("UserId")]
        public virtual Users? Users { get; set; }
    }
}
