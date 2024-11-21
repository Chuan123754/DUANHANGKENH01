using appAPI.IRepository;
using appAPI.Models;

namespace appAPI.Repository
{
    public class OrderTrackingRepository: IOrderTrackingRepository
    {
        private readonly APP_DATA_DATN _context;

        public OrderTrackingRepository(APP_DATA_DATN context)
        {
            _context = context;
        }

        public Task Create(order_trackings address)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<order_trackings>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<order_trackings> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<order_trackings> GetByOrderId(long idOrder)
        {
            throw new NotImplementedException();
        }

        public Task Update(order_trackings address, long id)
        {
            throw new NotImplementedException();
        }
    }
}
