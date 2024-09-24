using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appViews.Models
{
    public partial class Q_A
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Question { get; set; }

        public string? Answer { get; set; }

        public long Created_by { get; set; }

        public long Updated_by { get; set; }
        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
    }
}

