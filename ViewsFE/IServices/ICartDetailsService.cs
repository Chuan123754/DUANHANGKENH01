using Views.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppViews.IServices
{
    public interface ICartDetailsService
    {
        Task<List<Cart_details>> GetAll();
        Task<Cart_details> GetById(long id);
        Task Create(Cart_details cartDetails);
        Task Update(Cart_details cartDetails);
        Task Delete(long id);
    }
}
