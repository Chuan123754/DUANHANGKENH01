using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    [Table("product_post")]
    public partial class Product_Posts
    {
        [Key]
        public long Id { get; set; }

        [StringLength(255)]
        public string? Title { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string Slug { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public long? AuthorId { get; set; }
        public bool Deleted { get; set; }

        [StringLength(255)]
        public string? Type { get; set; }
        public string? product_video { get; set; }
        public string? Short_description { get; set; }
        public string ? Description { get; set; }
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
       
        public virtual ICollection<Post_tags> Post_tags { get; set; } = new List<Post_tags>();
      
        public virtual ICollection<Post_categories> Post_categories { get; set; } = new List<Post_categories>();
      
        [ForeignKey("AuthorId")]
        public virtual Designer? Designer { get; set; }
        [JsonIgnore]
        public virtual Banner? Banner { get; set; }

    }
}

