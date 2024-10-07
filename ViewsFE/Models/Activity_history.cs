using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewsFE.Models
{
    public class Activity_history
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Log_name { get; set; }

        [Required]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(50)]
        public string Subject_type { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
        [Required]
        public string AccountId { get; set; }  

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }


    }
}
