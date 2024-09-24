using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace appAPI.Repository
{
    public class AttributesRepon : IAttributesRepon
    {
        APP_DATA_DATN _context;
        public AttributesRepon()
        {
            _context = new APP_DATA_DATN();
        }
        public async Task Create(Attributes at)
        {
            at.Created_at = DateTime.Now;
            at.Updated_at = null;
            at.Deleted_at = null;
            _context.Attributes.Add(at);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = _context.Attributes.Find(id);
            _context.Attributes.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Attributes>> GetAll()
        {
            return await _context.Attributes.ToListAsync();
        }

        public async Task<Attributes> GetById(long id)
        {
            return await _context.Attributes.FindAsync(id);
        }

        public async Task<List<Attributes>> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return new List<Attributes>();
            }

            return await _context.Attributes
                .Where(f => f.Color.Contains(keyword) || f.Slug.Contains(keyword))
                .ToListAsync();
        }

        public async Task Update(Attributes at)
        {
            var item = _context.Attributes.Find(at.Id);
            if (item == null)
            {
                return;
            }

            item.Title = at.Title;
            item.Slug = at.Slug;
            item.Parent_Id = at.Parent_Id;
            item.Dept = at.Dept;
            item.Description = at.Description;
            item.Feature_Image = at.Feature_Image;
            item.Color = at.Color;
            item.Template = at.Template;
            item.In_Order = at.In_Order;
            item.Deleted_at = null;
            item.Created_at = at.Created_at;
            item.Updated_at = DateTime.Now;

            _context.Attributes.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
