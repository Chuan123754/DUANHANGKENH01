﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("Designer")]
    public class Designer
    {
        [Key]
        public long id_Designer { get; set; }
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Tên ngắn là bắt buộc.")]
        public string? ShortName { get; set; }
        [Required(ErrorMessage = "Đường dẫn là bắt buộc.")]
        public string? slug { get; set; }
        [Required(ErrorMessage = "Mô tả ngắn là bắt buộc.")]
        public string? short_description { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Ảnh đại diện là bắt buộc.")]
        public string? image { get; set; }
        public bool? Deleted { get; set; }
        public string? image_library { get; set; }
        public string? status { get; set; }
        public string? meta_data { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        [JsonIgnore]    
        public virtual ICollection<Product_Posts>? Product_Posts { get; set; }
        [JsonIgnore]
        public virtual Banner? Banner { get; set; }
   
        // Define constant values for statuses
        public const string STATUS_UNACTIVE = "UNACTIVE";
        public const string STATUS_ACTIVE = "ACTIVE";
        public const string STATUS_DELETE = "delete";

        // Dictionary to hold status labels
        public static readonly Dictionary<string, string> STATUSES = new Dictionary<string, string>
        {
            { STATUS_ACTIVE, "Hoạt động" },
            { STATUS_UNACTIVE, "Không hoạt động" },
            { STATUS_DELETE, "Đã xoá" }
        };

        // Dictionary to hold status classes (for styling purposes)
        public static readonly Dictionary<string, string> STATUS_CLASSES = new Dictionary<string, string>
        {
            { STATUS_UNACTIVE, "text-danger" },
            { STATUS_ACTIVE, "text-success" },
            { STATUS_DELETE, "text-muted" }
        };

        public string StatusLabel => !string.IsNullOrEmpty(status) && STATUSES.ContainsKey(status) ? STATUSES[status] : "Trạng thái không xác định";

        public string StatusClass => !string.IsNullOrEmpty(status) && STATUS_CLASSES.ContainsKey(status) ? STATUS_CLASSES[status] : string.Empty;
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
