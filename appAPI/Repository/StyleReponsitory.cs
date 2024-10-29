using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace appAPI.Repository
{
    public class StyleReponsitory : IStyleReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public StyleReponsitory()
        {
            _context = new APP_DATA_DATN();
        }
        public async Task Create(Style style)
        {
            style.Create_at = DateTime.Now;
            style.Deleted = false;
            _context.Add(style);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var post = await _context.Styles.FindAsync(id);
            if (post != null)
            {
                post.Deleted = true;
                post.Delete_at = DateTime.Now;
                _context.Styles.Update(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Style>> GetAll()
        {
            return await _context.Styles.Where(p => p.Deleted == false).ToListAsync();
        }


        public async Task<Style> GetByIdAndType(long id)
        {
            return await _context.Styles.FirstOrDefaultAsync(p => p.Id == id && p.Deleted == false);
        }

        public async Task<List<Style>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Styles
                .Where(p => (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm) && p.Deleted == false))
                .OrderBy(p => p.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm với điều kiện Deleted = false
            return await _context.Styles
                .CountAsync(p => p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task Update(Style style)
        {
            var item = _context.Styles.Find(style.Id);
            if (item == null)
            {
                return;
            }
            item.Title = style.Title;
            item.Slug = style.Slug;
            item.Description = style.Description;
            item.Update_at = DateTime.Now;
            _context.Styles.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
