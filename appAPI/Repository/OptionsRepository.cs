using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class OptionsRepository : IOptionsRepository
    {
        APP_DATA_DATN context;
        public OptionsRepository()
        {
            context = new APP_DATA_DATN();
        }
        public async Task Create(Options op)
        {
            context.Options.Add(op);
            await context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = await Details(id);
            context.Options.Remove(data);
        }

        public async Task<Options> Details(long id)
        {
            return await context.Options.FindAsync(id);
        }

        public async Task<List<Options>> GetAll()
        {
            return await context.Options.ToListAsync();
        }

        public async Task Update(Options op)
        {
            context.Entry(op).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
