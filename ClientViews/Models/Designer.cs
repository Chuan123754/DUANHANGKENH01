using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientViews.Models
{
    [Table("Designer")]
    public class Designer
    {
        [Key]
        public long id_Designer { get; set; }

        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? slug { get; set; }
        public string? short_description { get; set; }
        public string? description { get; set; }

        // Image stored as serialized JSON string
        public string? image { get; set; }

        // Image Library stored as serialized JSON string
        public string? image_library { get; set; }

        // Status field
        public string? status { get; set; }

        // Meta data stored as serialized JSON string
        public string? meta_data { get; set; }

        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product_Posts>? Product_Posts { get; set; }

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
        public string StatusLabel => STATUSES.ContainsKey(status) ? STATUSES[status] : string.Empty;

        // Get the CSS class for the current status
        public string StatusClass => STATUS_CLASSES.ContainsKey(status) ? STATUS_CLASSES[status] : string.Empty;

        // Deserialize MetaData from JSON string
        public MetaData GetMetaData()
        {
            return !string.IsNullOrEmpty(meta_data)
                ? JsonSerializer.Deserialize<MetaData>(meta_data)
                : new MetaData();
        }

        // Serialize MetaData to JSON string
        public void SetMetaData(MetaData data)
        {
            meta_data = JsonSerializer.Serialize(data);
        }

        // Deserialize ImageLibrary from JSON string
        public List<Image> GetImageLibrary()
        {
            return !string.IsNullOrEmpty(image_library)
                ? JsonSerializer.Deserialize<List<Image>>(image_library)
                : new List<Image>();
        }

        // Serialize ImageLibrary to JSON string
        public void SetImageLibrary(List<Image> images)
        {
            image_library = JsonSerializer.Serialize(images);
        }

        // Deserialize Image from JSON string
        public Image? GetImage()
        {
            return !string.IsNullOrEmpty(image)
                ? JsonSerializer.Deserialize<Image>(image)
                : null;
        }

        // Serialize Image to JSON string
        public void SetImage(Image img)
        {
            image = JsonSerializer.Serialize(img);
        }
    }

    public class Image
    {
        public long Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
    }

    public class MetaData
    {
        public string? facebook { get; set; }
        public string? twitter { get; set; }
        public string? instagram { get; set; }
        public string? youtube { get; set; }
        public string? tiktok { get; set; }
        public string? linkedin { get; set; }
        public string? pinterest { get; set; }
    }
}
