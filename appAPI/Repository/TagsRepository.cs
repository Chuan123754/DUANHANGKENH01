using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class TagsRepository : ITagsRepository
    {
        APP_DATA_DATN context;
        public TagsRepository(APP_DATA_DATN _context)
        {
            context = _context;
        }
        public async Task<List<Post_tags>> GetTagByPostId(long postId)
        {
            return await context.Post_Tags.Where(p => p.Post_Id == postId).Include(p=>p.Tag).ToListAsync();
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
