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
    [Table("Comments")]
    public class Comments
    {
        [Key]
        public long Id { get; set; }    
        public long Post_id { get; set; }
        public long User_id { get; set; }
        public long User_admin_id { get; set; }
        public string? Content { get; set; }
        [StringLength(255)]
        public string? Author_IP { get; set; }
        public long Parent { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [StringLength(20)]
        public string? Status { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get;set; }
        [ForeignKey("User_id")]
        [JsonIgnore]
        public virtual Users? Users { get; set; }
        [ForeignKey("Post_id")]
        public virtual Posts? Post { get; set; }
    }
}
