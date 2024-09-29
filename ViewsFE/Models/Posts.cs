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
    [Table("posts")]
    [Index("AuthorId", Name = "IX_posts_author_id")]
    [Index("Slug", Name = "IX_posts_slug")]
    [Index("Status", Name = "IX_posts_status")]
    [Index("Type", Name = "IX_posts_type")]
    [Index("Slug", "Deleted_at", Name = "UK_posts_slug_deleted_at", IsUnique = true)]
    public partial class Posts
    {
        [Key]
        public long Id { get; set; }

        [StringLength(255)]
        public string? Title { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Slug { get; set; }

        [StringLength(20)]
        public string? Status { get; set; }

        public long AuthorId { get; set; }

        [StringLength(255)]
        public string? Type { get; set; }

        public DateTime? Post_date { get; set; }

        public long Created_by { get; set; }

        public long Updated_by { get; set; }

        public DateTime? Deleted_at { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
        [InverseProperty("Posts")]
        [JsonIgnore]
        public virtual ICollection<Post_metas> Post_metas { get; set; }
        [InverseProperty("Posts")]
        [JsonIgnore]
        public virtual ICollection<Products> Post_products { get; set; } = new List<Products>();
        [InverseProperty("Posts")]
        [JsonIgnore]
        public virtual ICollection<Post_tags> Post_tags { get; set; }

        // Quan hệ với bảng Categories (nếu cần, nhưng thường không trực tiếp)
        public virtual ICollection<Categories> Categories { get; set; }

        [InverseProperty("Posts")]
        [JsonIgnore]
        public virtual ICollection<Post_categories> Post_categories { get; set; }

        // Quan hệ với bảng Tags (nếu cần, nhưng thường không trực tiếp)
        public virtual ICollection<Tags> Tags { get; set; }

        // Nội dung bài viết
        public string Content { get; set; }

        // để lấy Id bên View.
        public List<long> SelectedCategoryId { get; set; }
        public List<long> SelectedTagId { get; set; }

    }
}



