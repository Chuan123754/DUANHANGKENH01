using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IOrderTrackingRepository
    {
        public Task<List<order_trackings>> GetAll();
        public Task<order_trackings> GetById(long id);
        public Task<order_trackings> GetByOrderId(long idOrder);
        public Task Create(order_trackings address);
        public Task Update(order_trackings address, long id);
        public Task Delete(long id);
    }
}
