namespace appAPI.Models
{
    public class AccessView
    {
        public long Id { get; set; }    
        public DateTime AccessDate { get; set; }
        public long TotalViews { get; set; } = 0;
    }
}
