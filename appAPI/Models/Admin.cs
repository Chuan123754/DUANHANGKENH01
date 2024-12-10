using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace appAPI.Models
{
    public class Admin
    {
        [Key]
        public long Id { get; set; }
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Orders> CreatedOrders { get; set; } = new List<Orders>();
        [JsonIgnore]
        public virtual ICollection<Activity_history> Activity_history { get; set; } = new List<Activity_history>();
        [JsonIgnore]
        public virtual ICollection<Product_Posts> Posts { get; set; } = new List<Product_Posts>();
    }
}
