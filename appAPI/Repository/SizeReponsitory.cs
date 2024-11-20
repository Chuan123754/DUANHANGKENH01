using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace appAPI.Repository
{
    public class SizeReponsitory : ISizeReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public SizeReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Size size)
        {
            size.Create_at = DateTime.Now;
            size.Deleted = false;
            _context.Add(size);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var post = await _context.Sizes.FindAsync(id);
            if (post != null)
            {
                post.Deleted = true;
                post.Delete_at = DateTime.Now;
                _context.Sizes.Update(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Size>> GetAll()
        {
            return await _context.Sizes.Where(p => p.Deleted == false).ToListAsync();
        }


        public async Task<Size> GetByIdAndType(long id)
        {
            return await _context.Sizes.FindAsync(id);
        }

        public async Task<List<Size>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Sizes
                .Where(p => (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm) && p.Deleted == false))
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            return await _context.Sizes
                .CountAsync(p => p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task Update(Size size)
        {
            var item = _context.Sizes.Find(size.Id);
            if (item == null)
            {
                return;
            }
            item.Title = size.Title;
            item.Slug = size.Slug;
            item.Description = size.Description;
            item.Update_at = DateTime.Now;
            _context.Sizes.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
