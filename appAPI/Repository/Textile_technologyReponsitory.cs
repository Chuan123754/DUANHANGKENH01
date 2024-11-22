using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace appAPI.Repository
{
    public class Textile_technologyReponsitory : ITextile_technologyReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public Textile_technologyReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Textile_technology texti)
        {
            texti.Create_at = DateTime.Now;
            texti.Deleted = false;
            _context.Add(texti);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var post = await _context.Textile_Technologies.FindAsync(id);
            if (post != null)
            {
                post.Deleted = true;
                post.Delete_at = DateTime.Now;
                _context.Textile_Technologies.Update(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Textile_technology>> GetAll()
        {
            return await _context.Textile_Technologies.Where(p => p.Deleted == false).ToListAsync();
        }

        public async Task<Textile_technology> GetByIdAndType(long id)
        {
            return await _context.Textile_Technologies.FirstOrDefaultAsync(p => p.Id == id && p.Deleted == false);
        }

        public async Task<List<Textile_technology>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Textile_Technologies
                  .Where(p => (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)) && p.Deleted == false)
                  .OrderBy(p => p.Id)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            return await _context.Textile_Technologies
                .CountAsync(p => p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task Update(Textile_technology texti)
        {
            var item = _context.Textile_Technologies.Find(texti.Id);
            if (item == null)
            {
                return;
            }
            item.Title = texti.Title;
            item.Slug = texti.Slug;
            item.Description = texti.Description;
            item.Update_at = DateTime.Now;
            _context.Textile_Technologies.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
