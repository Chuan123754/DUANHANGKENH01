using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Views.Models
{
    [Table("post_metas")]
    [Index("Meta_key", Name = "IX_post_metas_meta_key")]
    public partial class Post_metas
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Meta_key { get; set; } = null!;
        public string? Meta_value { get; set; }
        [ForeignKey("Post_Id")]
        [InverseProperty("Post_metas")]
        [JsonIgnore]
        public virtual Posts? Posts { get; set; }
    }
}
