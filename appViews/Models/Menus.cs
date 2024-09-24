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
    [Table("menus")]
    public partial class Menus
    {
        [Key]
        public long Id { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Name { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Slug { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [JsonIgnore]
        [InverseProperty("Menus")]
        public virtual ICollection<Menu_items> Menu_Items { get; set; } = new List<Menu_items>();
    }
}