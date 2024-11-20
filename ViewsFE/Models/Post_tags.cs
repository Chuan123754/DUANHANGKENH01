using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("post_tags")]
    public partial class Post_tags
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        public long Tag_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Post_Id")]
        public virtual Product_Posts? Posts { get; set; }
        [ForeignKey("Tag_Id")]
        public virtual Tags? Tag { get; set; }
    }

}
