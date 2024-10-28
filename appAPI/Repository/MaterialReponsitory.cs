using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace appAPI.Repository
{
    public class MaterialReponsitory : IMaterialReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public MaterialReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Material mate)
        {
            mate.Create_at = DateTime.Now;
            mate.Deleted = false;
            _context.Add(mate);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var post = await _context.Materials.FindAsync(id);
            if (post != null)
            {
                post.Deleted = true;
                post.Delete_at = DateTime.Now;
                _context.Materials.Update(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Material>> GetAll()
        {
            return await _context.Materials.Where(p => p.Deleted == false).ToListAsync();
        }

        public async Task<Material> GetByIdAndType(long id)
        {
            return await _context.Materials.FirstOrDefaultAsync(p => p.Id == id && p.Deleted == false);
        }

        public async Task<List<Material>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Materials
                  .Where(p => (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm) && p.Deleted == false))
                  .OrderBy(p => p.Title)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm với điều kiện Deleted = false
            return await _context.Materials
                .CountAsync(p => p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task Update(Material mate)
        {
            var item = _context.Materials.Find(mate.Id);
            if (item == null)
            {
                return;
            }
            item.Title = mate.Title;
            item.Slug = mate.Slug;
            item.Description = mate.Description;
            item.Update_at = DateTime.Now;
            _context.Materials.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
