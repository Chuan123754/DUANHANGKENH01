using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewsFE.Models
{
    public class Activity_history
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Log_name { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject_type { get; set; }

        [Required]
        public string AccountId { get; set; }  // Đảm bảo kiểu dữ liệu phù hợp với IdentityUser

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
