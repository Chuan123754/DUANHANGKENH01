using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        APP_DATA_DATN _context;
        public CategoriesRepository(APP_DATA_DATN context)
        {
            _context = context;
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

        public async Task<List<Categories>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Categories
                .Where(p => p.Type == type && (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm) && p.Deleted == false))
                .OrderBy(p => p.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string type, string searchTerm)
        {
            return await _context.Categories
                .CountAsync(p => p.Type == type && p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
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
