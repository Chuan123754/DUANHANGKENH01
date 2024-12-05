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
            try
            {
                await _context.Orders.AddAsync(orders);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message); 
                throw;
            }
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
                .Include(o=>o.Payment)
                .ToListAsync();
        }

        public async Task<Orders> GetByIdOrders(long id)
        {
            return await _context.Orders
                .Include(o => o.Admin)
                .Include(o => o.Users)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == id); // Trả về null nếu không tìm thấy
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
                updateItem.TotalPrincipal = orders.TotalPrincipal;
                updateItem.FeeShipping = orders.FeeShipping;
                updateItem.Update_at = DateTime.Now;
                updateItem.TypePayment = orders.TypePayment;

                _context.Orders.Update(updateItem);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<int> GetTotal()
        {
            return await _context.Orders
               .CountAsync(p => p.Status != "Hóa đơn treo");
        }
        public async Task<int> GetTotalToday()
        {
            var today = DateTime.Today; // Lấy ngày hiện tại (00:00:00)
            return await _context.Orders
                .CountAsync(p => p.Status != "Hóa đơn treo" &&
                                 p.Created_at.HasValue && // Kiểm tra không null
                                 p.Created_at.Value.Date == today); // So sánh chỉ phần ngày
        }

        public async Task<decimal> GetTotalPiceToday()
        {
            var today = DateTime.Today; // Lấy ngày hiện tại (00:00:00)
            return await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" &&
                            p.Created_at.HasValue &&
                            p.Created_at.Value.Date == today) // So sánh phần ngày
                .SumAsync(p => p.TotalAmount ?? 0); // Tổng giá trị TotalAmount, mặc định là 0 nếu null
        }
        public async Task<decimal> GetTotalPiceWeek()
        {
            var today = DateTime.Today; // Ngày hiện tại (00:00:00)
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1); // Lấy ngày bắt đầu tuần (thứ Hai)
            var endOfWeek = startOfWeek.AddDays(6); // Lấy ngày cuối tuần (Chủ nhật)

            return await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" &&
                            p.Created_at.HasValue &&
                            p.Created_at.Value.Date >= startOfWeek &&
                            p.Created_at.Value.Date <= endOfWeek) // Lọc các hóa đơn trong tuần hiện tại
                .SumAsync(p => p.TotalAmount ?? 0); // Tổng giá trị TotalAmount, mặc định là 0 nếu null
        }

    }
}
