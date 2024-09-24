using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace appViews.Models
{
    [Table("post_tags")]
    [Index("Post_Id", Name = "IX_post_tags_post_id")]
    [Index("Tag_Id", Name = "IX_post_tags_tag_id")]
    public partial class Post_tags
    {
        public long Id { get; set; }
        public long Post_Id { get; set; }
        public long Tag_Id { get; set; }

        [ForeignKey("Post_Id")]
        [JsonIgnore]
        public virtual Posts? Posts { get; set; }

        [ForeignKey("Tag_Id")]
        [JsonIgnore]
        public virtual Tags? Tag { get; set; }
    }

}
