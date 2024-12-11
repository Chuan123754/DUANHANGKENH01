 using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class OrderDetailsReponsitory : OrderDetailsIReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public OrderDetailsReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task<Order_details> GetByOrderIdAndProductAttributeId(long orderId, long productAttributeId)
        {
            return await _context.Order_Details
                                 .FirstOrDefaultAsync(od => od.OrderId == orderId && od.Product_Attribute_Id == productAttributeId);
        }

        public async Task Create(Order_details orderdetails)
        {
           _context.Order_Details.Add(orderdetails);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var itemdelete = await GetByIdOrderdetails(id);
            _context.Order_Details.Remove(itemdelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order_details>> GetAlldetail()
        {
           return await _context.Order_Details
                .Include(o=>o.ProductAttributes)
                .ToListAsync();
        }

        public async Task<Order_details> GetByIdOrderdetails(long id)
        {
            return await _context.Order_Details.FindAsync(id);
        }
        public async Task<List<Order_details>> GetOrderDetailsByOrderId(long idOrder)
        {
            return await _context.Order_Details
                .Where(o=>o.OrderId == idOrder)
                .Include(o => o.Orders).ThenInclude(od => od.Users)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Color)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Size)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Product_Variant.Material)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Product_Variant.Textile_Technology)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Product_Variant.Style)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Product_Variant.Posts)
                .Include(o=>o.Orders)
                .ToListAsync();
        }

        public async Task Update(Order_details orderdetails, long id)
        {
            var updateItem = await GetByIdOrderdetails(id);
            if (updateItem != null)
            {
                updateItem.Quantity = orderdetails.Quantity;
                updateItem.TotalDiscount = orderdetails.TotalDiscount;
                _context.Order_Details.Update(updateItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order_details>> GetByTypeAsync(int pageNumber, int pageSize)
        {
            return await _context.Order_Details            
               .OrderBy(p => p.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Order_Details.CountAsync();
        }
    }
}
