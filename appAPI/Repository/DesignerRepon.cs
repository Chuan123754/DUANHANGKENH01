using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class DesignerRepon : IDesignerRepon
    {
        APP_DATA_DATN _context;
        public DesignerRepon()
        {
            _context = new APP_DATA_DATN();
        }
        public async Task Create(Designer at)
        {
            at.create_at = DateTime.Now;
            _context.Designer.Add(at);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = _context.Designer.Find(id);
            _context.Designer.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Designer>> GetAll()
        {
          return await _context.Designer.ToListAsync();
        }

        public async Task<Designer> GetById(long id)
        {
            return await _context.Designer.FindAsync(id);
           
        }

        public async Task<List<Designer>> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return new List<Designer>();
            }
            return await _context.Designer.Where(f => f.Name.Contains(keyword)).ToListAsync();
        }

        public async Task Update(Designer at)
        {
            var item = _context.Designer.Find(at.id_Designer);
            if (item == null)
            {
                return;
            }
            item.Name = at.Name;
            item.slug = at.slug;
            item.short_description = at.short_description;
            item.description = at.description;  
            item.image = at.image;
            item.image_library = at.image_library;
            item.status = at.status;
            item.meta_data = at.meta_data;
            item.create_at = at.create_at;
            item.update_at = DateTime.Now;
            _context.Designer.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
