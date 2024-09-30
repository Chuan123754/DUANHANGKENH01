using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        APP_DATA_DATN _context;
        public CategoriesRepository()
        {
            _context = new APP_DATA_DATN();
        }
        public async Task Create(Categories c)
        {
            _context.Categories.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = _context.Categories.Find(id);
            _context.Categories.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Categories> Details(long id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Categories>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Categories>> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return new List<Categories>();
            }

            return await _context.Categories
                .Where(f => f.Title.Contains(keyword) || f.Slug.Contains(keyword))
                .ToListAsync();
        }

        public async Task Update(Categories c)
        {
            if (_context.Categories.Any(c => c.Id == c.Id))
            {
                _context.Entry(c).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
