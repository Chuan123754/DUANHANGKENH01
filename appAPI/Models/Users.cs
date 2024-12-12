using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    [Table("users")]
    public partial class Users
    {
        [Key] 
        public long Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(255)]
        public string? Password { get; set; }
        [StringLength(int.MaxValue)]
        public string? RememberToken { get; set; }
        public bool OTPCheck { get; set; } = false;
        public string? Address { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        [JsonIgnore]
        public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
        [InverseProperty("Users")]
        [JsonIgnore]
        public Wishlist? Wishlist { get; set; }
        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        [JsonIgnore]
        public virtual ICollection<UserVouchers> UserVouchers { get; set; } = new List<UserVouchers>();
    }
}
