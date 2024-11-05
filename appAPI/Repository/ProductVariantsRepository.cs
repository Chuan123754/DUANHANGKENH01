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
            if (deleteItem != null)
            {
                _context.product_variants.Remove(deleteItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotImplementedException("Not Found");
            }
        }

        public async Task<List<Product_variants>> GetAllProductVarians()
        {
            var lst_Product = await _context.product_variants.ToListAsync();
            if (lst_Product != null)
            {
                return lst_Product;
            }
            else
            {
                throw new NotImplementedException("Not Found");
            }
        }

        public async Task<Product_variants> GetProductVariantsById(long id)
        {
            var product = await _context.product_variants.FindAsync(id);
            if(product != null)
            {
                return product;
            }
            else
            {
                throw new NotImplementedException("Not Found");
            }
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
                throw new NotImplementedException("Not Found");
            }
        }
    }
}
