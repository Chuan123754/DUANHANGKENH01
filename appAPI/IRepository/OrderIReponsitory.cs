using appAPI.Models;

namespace appAPI.IRepository
{
    public interface OrderIReponsitory
    {
        Task<List<Orders>> GetAll();
        Task<Orders> GetByIdOrders(long id);
        Task<object> GetByIdOrdersAddress(long id);
        Task<List<Orders>> GetOrderByIdAdmin(string idAdmin);
        Task<List<Orders>> GetOrderByIdUser(long  idUser);
        Task Create(Orders orders);
        Task Update(Orders orders , long id);
        Task UpdateSrees(Orders orders , long id);
        Task UpdateStatus(Orders orders, long id);
        Task Delete(long id);
        Task<int> GetTotal();
        Task<int> GetTotalToday();
        Task<decimal> GetTotalPiceToday();
        Task<decimal> GetTotalPiceWeek();
        Task<decimal> GetTotalPiceMonth();
        Task<decimal> GetTotalKhoangThoiGian(DateTime? TuNgay, DateTime? DenNgay);
        public Task<Dictionary<int, decimal>> GetTotalRevenuePerYear();
        public Task<Dictionary<string, int>> GetTotalMonth();

    }
}
