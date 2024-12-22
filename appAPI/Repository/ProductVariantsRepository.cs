//using appAPI.IRepository;
//using appAPI.Models;
//using Microsoft.EntityFrameworkCore;

//namespace appAPI.Repository
//{
//    public class ProductVariantsRepository : IProductVariantsRepository
//    {
//        private readonly APP_DATA_DATN _context;

//        public ProductVariantsRepository(APP_DATA_DATN context)
//        {
//            _context = context;
//        }
//        public async Task Create(Product_variants productVariants)
//        {
//            productVariants.Created_at = DateTime.Now;
//            await _context.product_variants.AddAsync(productVariants);
//            await _context.SaveChangesAsync();
//        }

//        public async Task Delete(long id)
//        {
//            var deleteItem = await _context.product_variants.FindAsync(id);
//            deleteItem.Status = "Đã xoá";
//            deleteItem.Deleted_at = DateTime.Now;
//            _context.product_variants.Update(deleteItem);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<List<Product_variants>> GetAllProductVarians()
//        {
//            return await _context.product_variants
//                  .Where(p => p.Status == "Hoạt động")
//                .Include(p => p.Posts)
//                .Include(p => p.Textile_Technology)
//                .Include(p => p.Material)
//                .Include(p => p.Style)
//                .ToListAsync();

//        }
//        public async Task<Product_variants> GetProductVariantsById(long id)
//        {
//            return await _context.product_variants
//                .Where(p => p.Status == "Hoạt động")
//                .Include(p => p.Posts)
//                .Include(p => p.Textile_Technology)
//                .Include(p => p.Material)
//                .Include(p => p.Style)
//                .FirstOrDefaultAsync(p => p.Id == id);
//        }

//        public async Task<int> GetTotalCountAsync(string status, string? searchTerm)
//        {
//            // Đếm tổng số sản phẩm trong bảng product_variants theo trạng thái và tìm kiếm với điều kiện Deleted_at là null
//            return await _context.product_variants
//                .CountAsync(p => p.Status == status && p.Deleted_at == null &&
//                                 (string.IsNullOrEmpty(searchTerm) || p.Description.Contains(searchTerm)));
//        }
//        public async Task<int> GetTotal()
//        {
//            return await _context.product_variants
//                .CountAsync(p => p.Status == "Hoạt động");
//        }


//        public async Task Update(Product_variants productVariants, long id)
//        {
//            var updateItem = await GetProductVariantsById(id);
//            if (updateItem != null)
//            {
//                updateItem.Post_Id = productVariants.Post_Id;
//                updateItem.Image = productVariants.Image;
//                updateItem.Description = productVariants.Description;
//                updateItem.Textile_technology_id = productVariants.Textile_technology_id;
//                updateItem.Material_id = productVariants.Material_id;
//                updateItem.Style_id = productVariants.Style_id;
//                updateItem.Created_at = productVariants.Created_at;
//                updateItem.Updated_at = DateTime.Now;
//                updateItem.Deleted_at = productVariants.Deleted_at;

//                await _context.SaveChangesAsync();
//            }
//            else
//            {
//                throw new KeyNotFoundException("Not Found");
//            }
//        }

//        public async Task<Product_variants?> FindVariant(long postId, byte textileTechnologyId, byte styleId, byte materialId)
//        {
//            return await _context.product_variants.Where(p => p.Status == "Hoạt động")
//                .FirstOrDefaultAsync(pv => pv.Post_Id == postId &&
//                                           pv.Textile_technology_id == textileTechnologyId &&
//                                           pv.Style_id == styleId &&
//                                           pv.Material_id == materialId);
//        }

//    }
//}
