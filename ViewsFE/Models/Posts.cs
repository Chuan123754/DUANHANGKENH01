using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("posts")]
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

        [JsonIgnore]
        public virtual ICollection<Post_metas> Post_metas { get; set; } = new List<Post_metas>();
        [JsonIgnore]
        public virtual ICollection<Product_variants> Product_Variants { get; set; } = new List<Product_variants>();
        [JsonIgnore]
        public virtual ICollection<Post_tags> Post_tags { get; set; } = new List<Post_tags>();
        [JsonIgnore]
        public virtual ICollection<Post_categories> Post_categories { get; set; } = new List<Post_categories>();
        [JsonIgnore]
        public virtual ICollection<Categories> Categories { get; set; } = new List<Categories>();

        // Quan hệ với bảng Tags
        public virtual ICollection<Tags> Tags { get; set; }

        // Nội dung bài viết
        public string Content { get; set; }

        // Danh sách ID các category đã chọn
        public List<long> SelectedCategoryId { get; set; } 

        // Danh sách ID các tag đã chọn
        public List<long> SelectedTagId { get; set; }

        // Phương thức để lấy SEO liên quan
        public virtual Seo Seo { get; set; } // thêm quan hệ với model Seo
    }
}
