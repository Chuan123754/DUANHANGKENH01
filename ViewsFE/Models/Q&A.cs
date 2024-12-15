using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewsFE.Models
{
    public partial class Q_A
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Câu hỏi là bắt buộc.")]
        public string? Question { get; set; }
        [Required(ErrorMessage = "Đáp án là bắt buộc.")]

        public string? Answer { get; set; }

        public long Created_by { get; set; }

        public long Updated_by { get; set; }
        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
        [NotMapped]
        public bool IsAnswerVisible { get; set; } = false;

    }
}

