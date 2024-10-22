using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appAPI.Models
{
    public class Address
    {
        [Key]
        public long Id { get; set; }
        public long User_Id { get; set; }
        public string Name { get; set; } // tên địa chỉ ( mặc định , địa chỉ 1 2 3 )
        public string Street { get; set; } // địa chỉ cụ thể 
        public string Ward_commune { get; set; }
        public string District { get; set; }
        public string Province_city { get; set; }
        public string Type { get; set; }
        public int Set_as_default { get; set; } = 1; // thiếp laajo địa chỉ mặc định 
        public string? Status { get; set; }
        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }
    }
}
