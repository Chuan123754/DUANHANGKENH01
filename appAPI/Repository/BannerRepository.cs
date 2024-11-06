using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly APP_DATA_DATN _context;

        public BannerRepository(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Banner banner)
        {
            banner.Created_at = DateTime.Now;
            _context.Banner.Add(banner);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var deleteItem = await _context.Banner.FindAsync(id);
            if (deleteItem != null)
            {
                _context.Banner.Remove(deleteItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Banner>> GetAllBanner()
        {
            var lstBanner = await _context.Banner.ToListAsync();
            return lstBanner;
        }

        public async Task<Banner> GetBannerById(long id)
        {
            var banner = await _context.Banner.FindAsync(id);
            return banner;
        }

        public async Task Update(Banner banner, long id)
        {
            var updateItem = await _context.Banner.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.Name = banner.Name;
                updateItem.Type = banner.Type;
                updateItem.Status = banner.Status;
                updateItem.Meta_data = banner.Meta_data;
                updateItem.Created_by = banner.Created_by;
                updateItem.Updated_by = banner.Updated_by;
                updateItem.Created_at= banner.Created_at;
                updateItem.Updated_at=DateTime.Now;

                _context.Banner.Update(updateItem);
                await _context.SaveChangesAsync();
            }

        }
    }
}
