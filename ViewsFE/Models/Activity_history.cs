using System.ComponentModel.DataAnnotations;
using Views.Models;

namespace ViewsFE.Models
{
    public class Activity_history
    {
        [Key]
        public long Id { get; set; }
        public string Log_name { get; set; }
        public string Descripcion { get; set; }
        public string Subject_type { get; set; }
        public string AccountId { get; set; }  // Đảm bảo kiểu dữ liệu phù hợp với IdentityUser
        public virtual Account Account { get; set; }
    }
}
