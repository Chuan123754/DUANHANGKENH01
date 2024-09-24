using Microsoft.EntityFrameworkCore;
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
    [Table("post_categories")]
    [Index("Category_Id", Name = "IX_post_categories_category_id")]
    [Index("Post_Id", Name = "IX_post_categories_post_id")]
    public partial class Post_categories
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        public long Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        [JsonIgnore]
        public virtual Categories? Categories { get; set; }
        [ForeignKey("Post_Id")]
        [JsonIgnore]
        public virtual Posts? Posts { get; set; }
    }
}
