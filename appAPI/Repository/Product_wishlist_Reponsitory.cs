using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class Product_wishlist_Reponsitory : IProduct_wishlist_Reponsitory
    {
        private readonly APP_DATA_DATN _context;
        public Product_wishlist_Reponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task<string> Create(Product_wishlist pw)
        {
            try
            {
                var existingPW = _context.Wishlist.Find(pw.Wishlist_id);
                if (existingPW == null)
                {
                    return  "Wishlist không tồn tại";
                }
                pw.Wishlist = existingPW;
                _context.Product_Wishlists.Add(pw);
                await _context.SaveChangesAsync();
                return "Tạo thành công";
            }
            catch (Exception ex)
            {
                return "Đã có lỗi xảy ra";
            }
        }

        public async Task<string> Delete(long id)
        {
            var delete = await _context.Product_Wishlists.FindAsync(id); 
            if (delete == null)
            {
                return "Sản phẩm không tồn tại trong wishlist";
            }

            _context.Product_Wishlists.Remove(delete);
            await _context.SaveChangesAsync(); 
            return "Xoá sản phẩm trong wishlist thành công";
        }


        public async Task<List<Product_wishlist>> GetAllPW()
        {
            var wlp = _context.Product_Wishlists
                 .Include(cd => cd.Wishlist)
                 .Include(cd => cd.Product_Posts)
                 .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(pa => pa.Color)
                 .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(pa => pa.Size)
                 .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(p => p.Style)
                 .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(p => p.Material)
                 .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(p => p.Textile_Technology)
                 .OrderByDescending(p => p.Product_Posts.Created_at)
                 .Select(p => new Product_wishlist
                 {
                     Id = p.Id,
                     Product_Posts_Id = p.Product_Posts_Id,
                     Product_Posts = p.Product_Posts,
                     Wishlist = p.Wishlist,
                     Wishlist_id = p.Wishlist_id,
                     MinPrice = p.Product_Posts.Product_Attributes.Min(pa => pa.Sale_price ?? pa.Regular_price), // Giá thấp nhất
                     MaxPrice = p.Product_Posts.Product_Attributes.Max(pa => pa.Sale_price ?? pa.Regular_price)
                 })
                 .ToListAsync();
            return await wlp;

        }
        public async Task<Product_wishlist> GetByID(long id)
        {
            var wlp = _context.Product_Wishlists
                .Where(p => p.Id == id)
                  .Include(cd => cd.Wishlist)
                  .Include(cd => cd.Product_Posts)
                  .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(pa => pa.Color)
                  .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(pa => pa.Size)
                  .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(p => p.Style)
                  .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(p => p.Material)
                  .Include(cd => cd.Product_Posts).ThenInclude(cm => cm.Product_Attributes).ThenInclude(p => p.Textile_Technology)
                  .OrderByDescending(p => p.Product_Posts.Created_at)
                  .Select(p => new Product_wishlist
                  {
                      Id = p.Id,
                      Product_Posts_Id = p.Product_Posts_Id,
                      Product_Posts = p.Product_Posts,
                      Wishlist = p.Wishlist,
                      Wishlist_id = p.Wishlist_id,
                      MinPrice = p.Product_Posts.Product_Attributes.Min(pa => pa.Sale_price ?? pa.Regular_price), // Giá thấp nhất
                      MaxPrice = p.Product_Posts.Product_Attributes.Max(pa => pa.Sale_price ?? pa.Regular_price)
                  })
                  .FirstAsync();
            return await wlp;
        }
    }
}
