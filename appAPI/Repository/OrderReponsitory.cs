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
            await _context.Orders.AddAsync(orders);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var itemdelete = await GetByIdOrders(id);
            _context.Orders.Remove(itemdelete);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Orders>> GetOrderByIdAdmin(string idAdmin)
        {
            return await _context.Orders.Where(o=>o.CreatedByAdminId == idAdmin).ToListAsync();
        }
        public async Task<List<Orders>> GetOrderByIdUser(long idUser)
        {
            return await _context.Orders.Where(o => o.User_id == idUser).ToListAsync();
        }
        public async Task<List<Orders>> GetAll()
        {
           return await _context.Orders
                .Include(o=>o.Admin)
                .Include(o=>o.Users)
                .ToListAsync();
        }

        public async Task<Orders> GetByIdOrders(long id)
        {
            return await _context.Orders
                .Include(o => o.Admin)
                .Include(o => o.Users)
                .FirstAsync(o=>o.Id==id);
        }

        public async Task Update(Orders orders, long id)
        {
            var updateItem = await _context.Orders.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.User_id = orders.User_id;
                updateItem.Status = orders.Status;
                updateItem.Note = orders.Note;
                updateItem.TotalAmount = orders.TotalAmount;
                updateItem.Totalmoney = orders.Totalmoney;
                updateItem.Update_at = DateTime.Now;

                _context.Orders.Update(updateItem);
                await _context.SaveChangesAsync();

            }
        }
    }
}
