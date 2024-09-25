using System.ComponentModel.DataAnnotations;

namespace appAPI.Models
{
    public class Activity_history
    {
        [Key]
        public long Id { get; set; }
        public string Log_name { get; set; }
        public string Descripcion { get; set; }
        public string Subject_type { get; set; }

    }
}
