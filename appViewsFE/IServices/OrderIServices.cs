using appViews.Models;

namespace AppViews.IServices
{
    public interface OrderIServices
    {
        Task<List<Orders>> GetAll();
        Task<Orders> GetByIdOrders(long id);
        Task Create(Orders orders);
        Task Update(Orders orders);
        Task Delete(long id);
        Task<List<Orders>> Search(string code);
    }
}
