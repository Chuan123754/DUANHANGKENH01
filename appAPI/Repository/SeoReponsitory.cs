using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace  appAPI.Repository
{
    public class SeoReponsitory : SeoIReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public SeoReponsitory()
        {
            _context = new APP_DATA_DATN();
        }
        public async Task CreateSeo(Seo seo)
        {
           _context.Seo.Add(seo);
            await _context.SaveChangesAsync();
        }

        public async Task<Seo> GetById(long id)
        {
            return await _context.Seo.FindAsync(id);
        }

        public async Task UpdateSeo(Seo seo)
        {
            _context.Entry(seo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
