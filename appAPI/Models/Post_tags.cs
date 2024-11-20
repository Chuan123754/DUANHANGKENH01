using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    [Table("post_tags")]
    public partial class Post_tags
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        public long Tag_Id { get; set; }

        [ForeignKey("Post_Id")]
        [JsonIgnore]
        public  Product_Posts? Posts { get; set; }
        [ForeignKey("Tag_Id")]
       
        public  Tags? Tag { get; set; }
    }

}
