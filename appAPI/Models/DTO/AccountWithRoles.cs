namespace appAPI.Models.DTO
{
    public class AccountWithRoles
    {
        public Account Account {  get; set; }
        public List<string> Roles { get; set; }
    }
}
