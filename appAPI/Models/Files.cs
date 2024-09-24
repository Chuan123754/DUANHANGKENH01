using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appAPI.Models
{
    [Table("files")]
    public partial class Files
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Slug { get; set; }
        [StringLength(255)]
        public string? Mine { get; set; }
        public int Size { get; set; }
        [StringLength(20)]
        public string? Ext { get; set; }
        [StringLength(255)]
        public string? Path { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get;set; }
    }
}
