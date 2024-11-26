using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class ProductAttributeDiscount : IProductAttributeDiscountRepository
    {
        private readonly APP_DATA_DATN _context;

        public ProductAttributeDiscount(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(P_attribute_discount attributeDiscount)
        {
            await _context.p_Variants_Discounts.AddAsync(attributeDiscount);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var deleteItem = await _context.p_Variants_Discounts.FindAsync(id);
            _context.p_Variants_Discounts.Remove(deleteItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<P_attribute_discount>> GetAll()
        {
            return await _context.p_Variants_Discounts
                .Include(a => a.ProductAttributes)
                .Include(a => a.Discount)
                .ToListAsync();
        }

        public async Task<P_attribute_discount> GetByIdAndType(long id)
        {
            return await _context.p_Variants_Discounts
                .Include(a => a.ProductAttributes)
                .Include(a => a.Discount)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<P_attribute_discount>> GetByIdDiscount(long idDiscount)
        {
            return await _context.p_Variants_Discounts
                .Where(a=>a.Discount_Id==idDiscount)   
                .Include(a => a.ProductAttributes)
                .Include(a => a.Discount)
                .ToListAsync();
        }

        public async Task<List<P_attribute_discount>> GetByIdProduct(long idProduct)
        {
            return await _context.p_Variants_Discounts
              .Where(a => a.P_attribute_Id == idProduct)
              .Include(a => a.ProductAttributes)
              .Include(a => a.Discount)
              .ToListAsync();
        }

        public Task Update(P_attribute_discount attributeDiscount, long id)
        {
            throw new NotImplementedException();
        }
    }
}
