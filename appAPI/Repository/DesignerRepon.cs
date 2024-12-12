using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class DesignerRepon : IDesignerRepon
    {
        APP_DATA_DATN _context;
        public DesignerRepon(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task<Designer> Create(Designer at)
        {
            at.Deleted = false;
            at.create_at = DateTime.Now;
            _context.Designer.Add(at);
            await _context.SaveChangesAsync();
            return at;
        }

        public async Task Delete(long id)
        {
            var data = _context.Designer.Find(id);
            data.Deleted = true;
            _context.Designer.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Designer>> GetAll()
        {
          return await _context.Designer.Where(p => p.Deleted == false && p.status == "ACTIVE").ToListAsync();
        }

        public async Task<Designer> GetById(long id)
        {
            return await _context.Designer.Where(p => p.id_Designer == id && p.Deleted == false).FirstOrDefaultAsync();
           
        }

        public async Task<List<Designer>> GetByTypeAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            // Lấy danh sách sản phẩm theo loại, phân trang và tìm kiếm
            return await _context.Designer
                .Where(p => p.Deleted == false && (string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)))
                .OrderBy(p => p.id_Designer) // Thay đổi theo tiêu chí sắp xếp bạn muốn
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync( string? searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm
            return await _context.Designer
                .CountAsync(p =>p.Deleted == false && (string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)));
        }

        public async Task<Designer> Update(Designer at)
        {
            var item = _context.Designer.Find(at.id_Designer);
            if (item == null)
            {
                return null;
            }
            item.Name = at.Name;
            item.ShortName = at.ShortName;
            item.slug = at.slug;
            item.short_description = at.short_description;
            item.description = at.description;  
            item.image = at.image;
            item.image_library = at.image_library;
            item.status = at.status;
            item.meta_data = at.meta_data;
            item.create_at = at.create_at;
            item.Deleted = false;
            item.update_at = DateTime.Now;
            _context.Designer.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
