using appAPI.Models;

namespace appAPI.IRepository
{
    public interface OrderIReponsitory
    {
        Task<List<Orders>> GetAll();
        Task<Orders> GetByIdOrders(long id);
        Task Create(Orders orders);
        Task Update(Orders orders);
        Task Delete(long id);
    }
}
