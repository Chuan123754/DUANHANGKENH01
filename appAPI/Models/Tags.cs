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
    [Table("tags")]
    public partial class Tags
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Title { get; set; }
        [StringLength(255)]
        public string? Slug { get; set; }
        [StringLength(100)]
        public string? Type { get; set; }
        public string? Description { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
      
        public virtual ICollection<Post_tags> Post_tags { get; set; } = new List<Post_tags>();

    }

}
