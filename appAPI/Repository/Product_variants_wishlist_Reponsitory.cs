using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class Product_variants_wishlist_Reponsitory : IProduct_variants_wishlist_Reponsitory
    {
        private readonly APP_DATA_DATN _context;
        public Product_variants_wishlist_Reponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task<string> Create(Product_variants_wishlist pw)
        {
            try
            {
                var existingPW = _context.Wishlist.Find(pw.Wishlist_id);
                if (existingPW == null)
                {
                    return  "Wishlist không tồn tại";
                }
                pw.Wishlist = existingPW;
                _context.Product_Variants_Wishlists.Add(pw);
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
           var delete = _context.Product_Variants_Wishlists.Find(id);
            if(delete == null)
            {
                return "Sản phẩm không tồn tại trong wishlist";
            }
            _context.Product_Variants_Wishlists.Remove(delete);
            _context.SaveChangesAsync();
            return "Xoá sản phẩm trong wishlist thành công";
        }

        public async Task<List<Product_variants_wishlist>> GetAllPW()
        {
            var wlp = _context.Product_Variants_Wishlists
                 .Include(cd => cd.Wishlist)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Posts)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Product_Attributes)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Product_Attributes).ThenInclude(pa => pa.Color)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Product_Attributes).ThenInclude(pa => pa.Size)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Style)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Material)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Textile_Technology)
                 .OrderByDescending(p => p.Product_Variants.Created_at)
                 .Select(p => new Product_variants_wishlist
                 {
                     Id = p.Id,
                     Product_variants_id = p.Product_variants_id,
                     Product_Variants = p.Product_Variants,
                     Wishlist = p.Wishlist,
                     Wishlist_id = p.Wishlist_id,
                     MinPrice = p.Product_Variants.Product_Attributes.Min(pa => pa.Sale_price ?? pa.Regular_price), // Giá thấp nhất
                     MaxPrice = p.Product_Variants.Product_Attributes.Max(pa => pa.Sale_price ?? pa.Regular_price)
                 })
                 .ToListAsync();
            return await wlp;

        }
        public async Task<Product_variants_wishlist> GetByID(long id)
        {
            var wlpId = _context.Product_Variants_Wishlists
                 .Include(cd => cd.Wishlist)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Posts)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Product_Attributes)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Product_Attributes).ThenInclude(pa => pa.Color)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Product_Attributes).ThenInclude(pa => pa.Size)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Style)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Material)
                 .Include(cd => cd.Product_Variants).ThenInclude(p => p.Textile_Technology)
                 .OrderByDescending(p => p.Product_Variants.Created_at)
                 .FirstOrDefault(cd => cd.Id == id);
            return wlpId;
        }
    }
}
