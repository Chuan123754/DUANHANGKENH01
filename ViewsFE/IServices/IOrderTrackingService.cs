using ViewsFE.Models;
using ViewsFE.Models.DTO;

namespace ViewsFE.IServices
{
    public interface IOrderTrackingService
    {
        Task<OrderTrackingDTO> GetBy(long Id);
        Task<List<Orders>> GetAllOrders();
        Task<string> AddOrderTracking(order_trackings newOrderTracking);
        Task<string> UpdateOrderTracking(long id, order_trackings updatedOrderTracking);
        Task<string> DeleteOrderTracking(long id);
    }
}
