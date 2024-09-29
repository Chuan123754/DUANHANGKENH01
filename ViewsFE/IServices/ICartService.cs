using ViewsFE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewsFE.IServices
{
    public interface ICartService
    {
        Task<List<Carts>> GetAll();
        Task<Carts> GetById(long id);
        Task Create(Carts cart);
        Task Update(Carts cart);
        Task Delete(long id);
    }
}
