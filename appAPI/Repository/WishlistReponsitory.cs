using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class WishlistReponsitory : IWishlistReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public WishlistReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Wishlist list)
        {
           list.Create_at = DateTime.Now;
           list.Deleted = false;
           _context.Add(list);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
          var list = await _context.Wishlist.FindAsync(id);
            if(list != null)
            {
                list.Deleted = true;
                list.Delete_at = DateTime.Now;
                _context.Wishlist.Update(list);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<Wishlist>> GetAll()
        {
            return await _context.Wishlist.Where(p => p.Deleted == false).ToListAsync();
        }

        public async Task<Wishlist> GetByIdAndType(long id)
        {
            return await _context.Wishlist.FindAsync(id);
        }

        public async Task<List<Wishlist>> GetByTypeAsync(int pageNumber, int pageSize)
        {
            return await _context.Wishlist
                .Where(p => p.Deleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm với điều kiện Deleted = false
            return await _context.Wishlist
                .CountAsync(p => p.Deleted == false);
        }
    }
}
