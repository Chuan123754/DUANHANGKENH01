﻿ using appAPI.IRepository;
using appAPI.Models;
using iText.StyledXmlParser.Jsoup.Nodes;
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
                 _context.Orders.Add(orders);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
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
            return await _context.Orders.Where(o => o.CreatedByAdminId == idAdmin).ToListAsync();
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
                .FirstOrDefaultAsync(o => o.Id == id); // Trả về null nếu không tìm thấy
        }

        public async Task<object> GetByIdOrdersAddress(long id)
        {
            var result = await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new
                {
                    OrderId = o.Id,
                    TotalAmount = o.TotalAmount,
                    TotalMoney = o.Totalmoney,
                    Status = o.Status,
                    CreatedAt = o.Created_at,
                    Address = new
                    {
                        AddressId = o.Address.Id,
                        Name = o.Address.Name,
                        Phone = o.Address.Phone,
                        Email = o.Address.Email,
                        Street = o.Address.Street,
                        WardCommune = o.Address.Ward_commune,
                        District = o.Address.District,
                        ProvinceCity = o.Address.Province_city,
                        Type = o.Address.Type,
                        SetAsDefault = o.Address.Set_as_default,
                        Status = o.Address.Status
                    }
                })
                .FirstOrDefaultAsync();
            return result;
        }



        public async Task Update(Orders orders, long id)
        {
            var updateItem = await _context.Orders.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.User_id = orders.User_id;
                updateItem.CreatedByAdminId = orders.CreatedByAdminId;
                updateItem.Address_Id = orders.Address_Id;
                updateItem.Status = orders.Status;
                updateItem.Note = orders.Note;
                updateItem.TotalAmount = orders.TotalAmount;
                updateItem.TotalVoucher = orders.TotalVoucher;
                updateItem.Totalmoney = orders.Totalmoney;
                updateItem.TotalPrincipal = orders.TotalPrincipal;
                updateItem.FeeShipping = orders.FeeShipping;
                updateItem.Update_at = DateTime.Now;
                updateItem.Created_at = DateTime.Now;
                updateItem.TypePayment = orders.TypePayment;
                updateItem.Strees = orders.Strees;
                _context.Orders.Update(updateItem);
                await _context.SaveChangesAsync();

            }
        }

        public async Task UpdateStatus(Orders orders, long id)
        {
            var updateItem = await _context.Orders.FindAsync(id);
            if (updateItem != null)
            {

                updateItem.Status = orders.Status;
                updateItem.Update_at = DateTime.Now;

                _context.Orders.Update(updateItem);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<int> GetTotal()
        {
            return await _context.Orders
               .CountAsync(p => p.Status != "Hóa đơn treo" && p.Status != "Pending");
        }
        public async Task<int> GetTotalToday()
        {
            var today = DateTime.Today; // Lấy ngày hiện tại (00:00:00)
            return await _context.Orders
                .CountAsync(p => p.Status != "Hóa đơn treo" && p.Status != "Pending" &&
                                 p.Created_at.HasValue && // Kiểm tra không null
                                 p.Created_at.Value.Date == today); // So sánh chỉ phần ngày
        }

        public async Task<decimal> GetTotalPiceToday()
        {
            var today = DateTime.Today; // Lấy ngày hiện tại (00:00:00)
            return await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" && p.Status != "Chờ xác nhận" && p.Status != "Pending" && p.Status != "Đơn hủy" && p.Status != "Giao thất bại" &&
                            p.Created_at.HasValue &&
                            p.Created_at.Value.Date == today) // So sánh phần ngày
                .SumAsync(p => (p.Totalmoney ?? 0) - (p.FeeShipping ?? 0));  // Tổng giá trị TotalAmount, mặc định là 0 nếu null
        }
        public async Task<decimal> GetTotalPiceWeek()
        {
            var today = DateTime.Today; // Ngày hiện tại (00:00:00)
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1); // Lấy ngày bắt đầu tuần (thứ Hai)
            var endOfWeek = startOfWeek.AddDays(6); // Lấy ngày cuối tuần (Chủ nhật)

            return await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" && p.Status != "Chờ xác nhận" && p.Status != "Pending" && p.Status != "Đơn huỷ" && p.Status != "Giao thất bại" && 
                            p.Created_at.HasValue &&
                            p.Created_at.Value.Date >= startOfWeek &&
                            p.Created_at.Value.Date <= endOfWeek) // Lọc các hóa đơn trong tuần hiện tại
                .SumAsync(p => (p.Totalmoney ?? 0) - (p.FeeShipping ?? 0)); // Tổng giá trị TotalAmount, mặc định là 0 nếu null
        }

        public async Task<decimal> GetTotalPiceMonth()
        {
            var today = DateTime.Today; // Ngày hiện tại (00:00:00)
            var startOfMonth = new DateTime(today.Year, today.Month, 1); // Ngày bắt đầu tháng (1 của tháng hiện tại)
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Ngày cuối cùng của tháng hiện tại

            return await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" && p.Status != "Chờ xác nhận" && p.Status != "Pending" && p.Status != "Đơn huỷ" && p.Status != "Giao thất bại" &&
                            p.Created_at.HasValue &&
                            p.Created_at.Value.Date >= startOfMonth &&
                            p.Created_at.Value.Date <= endOfMonth) // Lọc hóa đơn trong tháng hiện tại
                 .SumAsync(p => (p.Totalmoney ?? 0) - (p.FeeShipping ?? 0));  // Tổng giá trị TotalAmount, mặc định là 0 nếu null
        }
        public async Task<Dictionary<int, decimal>> GetTotalRevenuePerYear()
        {
            var orders = await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" && p.Status != "Chờ xác nhận" && p.Status != "Pending" && p.Status != "Đã huỷ" && p.Status != "Giao thất bại" && p.Created_at.HasValue) // Lọc theo trạng thái và chắc chắn có giá trị ngày tạo
                .GroupBy(p => p.Created_at.Value.Year) // Nhóm theo năm của ngày tạo
                .Select(g => new
                {
                    Year = g.Key,
                    TotalRevenue = g.Sum(p => (p.Totalmoney ?? 0) - (p.FeeShipping ?? 0)) // Tính tổng doanh thu cho từng năm
                })
                .ToListAsync();

            // Tạo dictionary để lưu doanh thu theo từng năm
            var revenuePerYear = new Dictionary<int, decimal>();

            foreach (var order in orders)
            {
                revenuePerYear[order.Year] = order.TotalRevenue; // Lưu doanh thu vào dictionary
            }

            return revenuePerYear;
        }

        public async Task<Dictionary<string, int>> GetTotalMonth()
        {
            var currentYear = DateTime.Now.Year;

            var orders = await _context.Orders
                .Where(o => o.Status != "Hóa đơn treo" && o.Status != "Pending" && o.Created_at != null && o.Created_at.Value.Year == currentYear)
                .ToListAsync();

            var result = orders
                .GroupBy(o => new { Year = o.Created_at.Value.Year, Month = o.Created_at.Value.Month })
                .Select(g => new
                {
                    YearMonth = $"{g.Key.Year}-{g.Key.Month:D2}",
                    TotalOrders = g.Count()
                })
                .ToDictionary(x => x.YearMonth, x => x.TotalOrders);

            return result;
        }

        public async Task UpdateSrees(Orders orders, long id)
        {
            var updateItem = await _context.Orders.FindAsync(id);
            if (updateItem != null)
            {           
                updateItem.Strees = orders.Strees;
                _context.Orders.Update(updateItem);
                await _context.SaveChangesAsync();

            }
        }
        public async Task<decimal> GetTotalKhoangThoiGian(DateTime? TuNgay, DateTime? DenNgay)
        {
            // Đảm bảo TuNgay và DenNgay là ngày bắt đầu và kết thúc của ngày tương ứng
            // Kiểm tra nếu TuNgay và DenNgay không null
            var startDate = TuNgay.HasValue ? TuNgay.Value.Date : (DateTime?)null;
            var endDate = DenNgay.HasValue ? DenNgay.Value.Date.AddDays(1).AddTicks(-1) : (DateTime?)null;

            return await _context.Orders
                .Where(p => p.Status != "Hóa đơn treo" && p.Status != "Chờ xác nhận" && p.Status != "Pending" &&
                            p.Status != "Đơn huỷ" && p.Status != "Giao thất bại" &&
                            p.Created_at.HasValue &&
                            p.Created_at.Value >= startDate &&
                            p.Created_at.Value <= endDate) // Lọc hóa đơn trong khoảng thời gian
                .SumAsync(p => (p.Totalmoney ?? 0) - (p.FeeShipping ?? 0)); // Tổng giá trị TotalAmount, mặc định là 0 nếu null
        }

    }
}