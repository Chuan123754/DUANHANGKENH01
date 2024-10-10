using System.ComponentModel.DataAnnotations;

namespace ViewsFE.Models
{
    public class Textile_technology
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        public DateTime Delete_at { get; set; }
        public virtual ICollection<Product_Posts> Product_Posts { get; set; } = new List<Product_Posts>();
        // Define constant values for statuses
        public const string STATUS_UNACTIVE = "UNACTIVE";
        public const string STATUS_ACTIVE = "ACTIVE";

        // Dictionary to hold status labels
        public static readonly Dictionary<string, string> STATUSES = new Dictionary<string, string>
        {
            { STATUS_ACTIVE, "Hoạt động" },
            { STATUS_UNACTIVE, "Không hoạt động" }
        };

        // Dictionary to hold status classes (for styling purposes)
        public static readonly Dictionary<string, string> STATUS_CLASSES = new Dictionary<string, string>
        {
            { STATUS_UNACTIVE, "text-danger" },
            { STATUS_ACTIVE, "text-success" }
        };

        // Get the label for the current status
        public string StatusLabel => STATUSES.ContainsKey(Status) ? STATUSES[Status] : string.Empty;

        // Get the CSS class for the current status
        public string StatusClass => STATUS_CLASSES.ContainsKey(Status) ? STATUS_CLASSES[Status] : string.Empty;
    }
}
