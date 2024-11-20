using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("menuitems")]
    public partial class Menu_items
    {
        [Key]
        public long id { get; set; }

        [StringLength(255)]
        public string? Label { get; set; }

        [StringLength(255)]
        public string? Link { get; set; }
        public long Parent { get; set; }
        public int Sort { get; set; }

        [StringLength(255)]
        public string? Class { get; set; }

        public long MenuId { get; set; }
        public int Depth { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [ForeignKey("MenuId")]
        [InverseProperty("Menu_Items")]
        public virtual Menus? Menus { get; set; }
    }
}
