using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace appViews.Models
{
    [Table("wishlist")]
    public class Wishlist
    {
        [Key]
        public long Id { get; set; }    
        public long User_id { get; set; }
        public long Product_id { get; set; }
        public DateTime? Create_at { get; set; }
        public DateTime? Updated_at { get; set; }
        [ForeignKey("Product_id")]
        [JsonIgnore]
        public virtual Products? Post_Products { get; set; }
        [ForeignKey("User_id")]
        [JsonIgnore]
        public virtual Users? Users { get; set; }

    }
}
