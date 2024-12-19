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
            c.Deleted = false;
            c.Created_at = DateTime.Now;
            _context.Categories.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTypePost(Categories c)
        {
            c.Created_at = DateTime.Now;
            c.Type = "post";
            c.Deleted = false;
            _context.Categories.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTypeProduct(Categories c)
        {
            c.Created_at = DateTime.Now;
            c.Type = "product";
            c.Deleted = false;
            _context.Categories.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTypeProject(Categories c)
        {
            c.Created_at = DateTime.Now;
            c.Type = "project";
            c.Deleted = false;
            _context.Categories.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = _context.Categories.Find(id);
            data.Deleted = true;
            data.Deleted_at = DateTime.Now;
            _context.Categories.Update(data);
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
        public async Task<List<Post_categories>> GetCategoryByPosstId(long postId)
        {
            return await _context.Post_Categories.Where(c => c.Post_Id == postId).Include(c => c.Categories).ToListAsync();
        }
        public async Task<List<Categories>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Categories
                .Where(p => p.Type == type && (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)) &&   p.Deleted == false)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string type, string searchTerm)
        {
            return await _context.Categories
                .CountAsync(p => p.Type == type &&   p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task Update(Categories c)
        {
            var item = await _context.Categories.FindAsync(c.Id);
            if (_context.Categories.Any(c => c.Id == c.Id))
            {
                _context.Entry(c).State = EntityState.Modified;
                if (item == null) return;
                item.Short_title = c.Short_title;
                item.Description = c.Description;
                item.Title = c.Title;
                item.Slug = c.Slug;
                item.Parent_id = c.Parent_id;
                item.Updated_at = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Categories>> GetAllType(string type)
        {
            return await _context.Categories.Where(p => p.Type == type &&   p.Deleted == false).ToListAsync();
        }
    }
}
