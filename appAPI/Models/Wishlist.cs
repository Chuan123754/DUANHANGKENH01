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
    [Table("wishlist")]
    public class Wishlist
    {
        [Key]
        public long Id { get; set; }    
        public long User_id { get; set; }
        public DateTime? Create_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Delete_at { get; set; }
        [ForeignKey("User_id")]
        public virtual Users? Users { get; set; }
    }
}
