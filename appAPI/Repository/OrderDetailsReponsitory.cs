 using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class OrderDetailsReponsitory : OrderDetailsIReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public OrderDetailsReponsitory()
        {
            _context = new APP_DATA_DATN();
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
           return await _context.Order_Details.ToListAsync();
        }

        public async Task<Order_details> GetByIdOrderdetails(long id)
        {
            return await _context.Order_Details.FindAsync(id);
        }


        public async Task Update(Order_details orderdetails)
        {
            _context.Entry(orderdetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
