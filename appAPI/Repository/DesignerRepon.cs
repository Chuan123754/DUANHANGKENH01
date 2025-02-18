﻿using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
           
            at.create_at = DateTime.Now;
            _context.Designer.Add(at);
            await _context.SaveChangesAsync();
            return at;
        }

        public async Task Delete(long id)
        {
            var data = _context.Designer.Find(id);
            data.status = "delete";
            _context.Designer.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Designer>> GetAll()
        {
          return await _context.Designer.Where(p => p.status != "delete").ToListAsync();
        }

        public async Task<Designer> GetById(long id)
        {
            return await _context.Designer.Where(p => p.id_Designer == id &&   p.status != "delete").FirstOrDefaultAsync();
           
        }

        public async Task<List<Designer>> GetByTypeAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            // Lấy danh sách sản phẩm theo loại, phân trang và tìm kiếm
            return await _context.Designer
                .Where(p =>   p.status != "delete" && (string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)))
                .OrderBy(p => p.id_Designer) // Thay đổi theo tiêu chí sắp xếp bạn muốn
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync( string? searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm
            return await _context.Designer
                .CountAsync(p =>  p.status != "delete" && (string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)));
        }
        public async Task<List<Designer>> GetByTypeAsyncClient(int pageNumber, int pageSize, string? searchTerm)
        {
            // Lấy danh sách sản phẩm theo loại, phân trang và tìm kiếm
            return await _context.Designer
                .Where(p =>  p.status == "ACTIVE" && (string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)))
                .OrderBy(p => p.id_Designer) // Thay đổi theo tiêu chí sắp xếp bạn muốn
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsyncClient(string? searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm
            return await _context.Designer
                .CountAsync(p =>  p.status == "ACTIVE" && (string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)));
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
            item.update_at = DateTime.Now;
            _context.Designer.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<Designer>> GetAllAC()
        {
            return await _context.Designer.Where(p => p.status == "ACTIVE").ToListAsync();
        }

        public async Task<Designer> GetByIdSlug(string slug)
        {
            return await _context.Designer.Where(p => p.slug == slug &&   p.status != "delete").FirstOrDefaultAsync();
        }

        public async Task<bool> CheckSlugForUpdate(string slug, long desiId)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            // Tìm kiếm Slug trùng lặp nhưng bỏ qua chính bản ghi hiện tại
            return await _context.Designer
                .AnyAsync(p => p.slug == slug && p.status != "delete" && p.id_Designer != desiId);
        }
    }
}
