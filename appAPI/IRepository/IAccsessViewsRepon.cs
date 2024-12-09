using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IAccsessViewsRepon
    {
        Task CountViewsAccsess ();
        Task<long> GetTotal();
    }
   
}
