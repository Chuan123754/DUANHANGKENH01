using appAPI.Models;

namespace appAPI.IRepository
{
    public interface OrderIReponsitory
    {
        Task<List<Orders>> GetAll();
        Task<Orders> GetByIdOrders(long id);
        Task<List<Orders>> GetOrderByIdAdmin(string idAdmin);
        Task<List<Orders>> GetOrderByIdUser(long  idUser);
        Task Create(Orders orders);
        Task Update(Orders orders , long id);
        Task Delete(long id);
    }
}
