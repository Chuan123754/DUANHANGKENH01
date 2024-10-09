using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewsFE.Models
{
    public class User_Vouchers
    {
        [Key]
        public long Id { get; set; }    
        public long UserId { get; set; }
        public long VoucherId { get; set; } 
        public string Status { get; set; }  
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? Users { get; set; }
        [ForeignKey("VoucherId")]
        public virtual Vouchers? Vouchers { get; set; }
    }
}
