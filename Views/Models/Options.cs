using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Models
{
    [Table("options")]
    [Index("Optin_name", Name = "UQ__options__A2E9140E2B971836", IsUnique = true)]
    public partial class Options
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Optin_name { get; set; }
        [StringLength(255)]
        public string? Option_value { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
