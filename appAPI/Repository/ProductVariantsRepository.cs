using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class ProductVariantsRepository : IProductVariantsRepository
    {
        private readonly APP_DATA_DATN _context;

        public ProductVariantsRepository(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Product_variants productVariants)
        {
            await _context.product_variants.AddAsync(productVariants);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var deleteItem = await _context.product_variants.FindAsync(id);
            _context.product_variants.Remove(deleteItem);
            await _context.SaveChangesAsync();  
        }

        public async Task<List<Product_variants>> GetAllProductVarians()
        {
            return await _context.product_variants.ToListAsync();

        }

        public async Task<Product_variants> GetProductVariantsById(long id)
        {
            return await _context.product_variants.FindAsync(id);
        }

        public async Task Update(Product_variants productVariants, long id)
        {
            var updateItem = await GetProductVariantsById(id);
            if (updateItem != null)
            {
                updateItem.Post_Id = productVariants.Post_Id;
                updateItem.Image=productVariants.Image;
                updateItem.Sku = productVariants.Sku;
                updateItem.Status = productVariants.Status;
                updateItem.Regular_price = productVariants.Regular_price;
                updateItem.Sale_price = productVariants.Sale_price;
                updateItem.Description = productVariants.Description;
                updateItem.Textile_technology_id = productVariants.Textile_technology_id;
                updateItem.Material_id = productVariants.Material_id;
                updateItem.Style_id = productVariants.Style_id;
                updateItem.Created_at = productVariants.Created_at;
                updateItem.Updated_at = DateTime.Now;
                updateItem.Deleted_at = productVariants.Deleted_at;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Not Found");
            }    
        }
    }
}
