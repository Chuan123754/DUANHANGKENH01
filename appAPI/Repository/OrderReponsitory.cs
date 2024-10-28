using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace appAPI.Repository
{
    public class OrderReponsitory : OrderIReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public OrderReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var itemdelete = await GetByIdOrders(id);
            _context.Orders.Remove(itemdelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Orders>> GetAll()
        {
           return await _context.Orders.ToListAsync();
        }

        public async Task<Orders> GetByIdOrders(long id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Orders>> Search(string code)
        {

            if (string.IsNullOrEmpty(code))
            {
                return new List<Orders>();
            }
            return await _context.Orders
            .Where(f => f.Code.Contains(code)).ToListAsync();
        }

        public async Task Update(Orders orders)
        {
            _context.Entry(orders).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
