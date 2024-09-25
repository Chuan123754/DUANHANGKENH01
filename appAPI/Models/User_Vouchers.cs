using System.ComponentModel.DataAnnotations;

namespace appAPI.Models
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

    }
}
