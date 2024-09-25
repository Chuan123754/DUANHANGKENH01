using System.ComponentModel.DataAnnotations;

namespace appAPI.Models
{
    public class Address
    {
        [Key]
        public long Id { get; set; }
        public long User_Id { get; set; }
        public string Name { get; set; }
        public string Steet { get; set; }
        public string Ward_commune { get; set; }
        public string District { get; set; }
        public string Province_city { get; set; }
        public string Type { get; set; }
        public int Set_as_default { get; set; }
        public string Status { get; set; }
    }
}
