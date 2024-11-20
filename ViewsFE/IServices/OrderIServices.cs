using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface OrderIServices
    {
        Task<List<Orders>> GetAll();
        Task<List<Orders>> GetOrderByIdAdmin(string idAdmin);
        Task<List<Orders>> GetOrderByIdUser(long idUser);
        Task<Orders> GetByIdOrders(long id);
        Task Create(Orders orders);
        Task Update(Orders orders , long id);
        Task Delete(long id);
    }
}
