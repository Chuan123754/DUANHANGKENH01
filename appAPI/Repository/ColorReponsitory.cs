using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class ColorReponsitory : IColorReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public ColorReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Color color)
        {
            color.Create_at = DateTime.Now;
            color.Deleted = false;
            _context.Add(color);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var post = await _context.Color.FindAsync(id);
            if (post != null)
            {
                post.Deleted = true;
                post.Delete_at = DateTime.Now;
                _context.Color.Update(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Color>> GetAll()
        {
            return await _context.Color.Where(p => p.Deleted == false).ToListAsync();
        }

        public async Task<Color> GetByIdAndType(long id)
        {
            return await _context.Color.FindAsync(id);
        }

        public async Task<List<Color>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Color
                 .Where(p => (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm) && p.Deleted == false))
                 .OrderBy(p => p.Title)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm với điều kiện Deleted = false
            return await _context.Color
                .CountAsync(p => p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }
        public async Task Update(Color color)
        {
            var item = _context.Color.Find(color.Id);
            if(item == null)
            {
                return;
            }
            item.Title = color.Title;
            item.Slug = color.Slug;
            item.Description = color.Description;
            item.Update_at = DateTime.Now;
            _context.Color.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
