using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace appAPI.Models
{
    public class Account : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Activity_history> Activity_history { get; set; } = new List<Activity_history>();
        [JsonIgnore]
        public virtual ICollection<Warehouse> Warehouse { get; set; } = new List<Warehouse>();
        [JsonIgnore]
        public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
    }
}
