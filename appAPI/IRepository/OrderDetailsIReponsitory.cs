using appAPI.Models;

namespace AppAPI.IRepository
{
    public interface OrderDetailsIReponsitory
    {
        Task<List<Order_details>> GetAlldetail();
        Task<Order_details> GetByIdOrderdetails(long id);
        Task Create(Order_details orderdetails);
        Task Update(Order_details orderdetails);
        Task Delete(long id);
    }
}
