using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("product_post")]
    public partial class Product_Posts
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        public string? Title { get; set; }
        public int? STT { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [Required(ErrorMessage = "Đường dẫn là bắt buộc.")]
        public string? Slug { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        public string? Status { get; set; }
        public long? AuthorId { get; set; }
        public bool? Deleted { get; set; }

        [StringLength(255)]
        public string? Type { get; set; }
        public string? product_video { get; set; }
        public string? Short_description { get; set; }
        public string? Description { get; set; }
        public string? Image_library { get; set; }
        public string? Feature_image { get; set; }

        public DateTime? Post_date { get; set; }

        public long? Created_by { get; set; }

        public long? Updated_by { get; set; }

        public DateTime? Deleted_at { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product_variants> Product_Variants { get; set; } = new List<Product_variants>();

        public virtual List<Post_tags> Post_tags { get; set; } = new List<Post_tags>();

        public virtual List<Post_categories> Post_categories { get; set; } = new List<Post_categories>();

        [ForeignKey("AuthorId")]
        public virtual Designer? Designer { get; set; }
        [JsonIgnore]
        public virtual Banner? Banner { get; set; }

        // Define constant values for statuses
        public const string STATUS_DRAFT = "draft";
        public const string STATUS_PENDING = "pending";
        public const string STATUS_PUBLISH = "publish";

        // Dictionary to hold status labels
        public static readonly Dictionary<string, string> STATUSES = new Dictionary<string, string>
        {
            { STATUS_DRAFT, "Nháp" },
            { STATUS_PENDING, "Hạ xuống" },
             { STATUS_PUBLISH, "Công khai" }
        };

        // Dictionary to hold status classes (for styling purposes)
        public static readonly Dictionary<string, string> STATUS_CLASSES = new Dictionary<string, string>
        {
           { STATUS_DRAFT, "text-danger" },
            { STATUS_PENDING, "text-warning" },
             { STATUS_PUBLISH, "text-success" }
        };

        // Get the label for the current status
        public string StatusLabel => STATUSES.ContainsKey(Status) ? STATUSES[Status] : string.Empty;

        // Get the CSS class for the current status
        public string StatusClass => STATUS_CLASSES.ContainsKey(Status) ? STATUS_CLASSES[Status] : string.Empty;

    }
}

