using appViews.Models;

namespace AppViews.IServices
{
    public interface OrderDetailsIServices
    {
        Task<List<Order_details>> GetAlldetail();
        Task<Order_details> GetByIdOrderdetails(long id);
        Task Create(Order_details orderdetails);
        Task Update(Order_details orderdetails);
        Task Delete(long id);
    }
}
