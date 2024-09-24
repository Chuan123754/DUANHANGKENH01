using Views.IServices;
using Views.Models;

namespace AppViews.Services
{
    public class OrderServices : OrderIServices
    {
        public Task Create(Orders orders)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Orders>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Orders> GetByIdOrders(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Orders>> Search(string code)
        {
            throw new NotImplementedException();
        }

        public Task Update(Orders orders)
        {
            throw new NotImplementedException();
        }
    }
}
