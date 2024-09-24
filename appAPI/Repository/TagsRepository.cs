using AppAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class TagsRepository : ITagsRepository
    {
        APP_DATA_DATN context;
        public TagsRepository()
        {
            context = new APP_DATA_DATN();
        }
        public async Task Create(Tags tag)
        {
            context.Tags.Add(tag);
            await context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = await context.Tags.FindAsync(id);
            context.Tags.Remove(data);
            await context.SaveChangesAsync();
        }

        public async Task<Tags> Details(long id)
        {
            return await context.Tags.FindAsync(id);
        }

        public async Task<List<Tags>> GetAll()
        {
            return await context.Tags.ToListAsync();
        }

        public async Task Update(Tags tag)
        {
            context.Entry(tag).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
