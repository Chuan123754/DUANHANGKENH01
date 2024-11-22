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
        // Thêm banner vào bài viết sau khi tạo bài viết
        public async Task AddBannerToPost(long postId, Banner banner)
        {
            banner.ProductPostId = postId;
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

        public async Task Update(Banner banner, long postId)
        {
            var updateItem = await _context.Banner.FirstOrDefaultAsync(b => b.ProductPostId == postId && b.Id == banner.Id);
            if (updateItem == null)
            {
                throw new Exception("Banner không tồn tại hoặc không liên kết với bài viết.");
            }

            updateItem.Name = banner.Name;
            updateItem.Meta_data = banner.Meta_data;
            updateItem.Updated_by = banner.Updated_by;
            updateItem.Updated_at = DateTime.Now;

            _context.Banner.Update(updateItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Banner> GetBannerByProductPostId(long PostId)
        {
            return await _context.Banner.Include(p => p.Product_Post).FirstOrDefaultAsync(p => p.ProductPostId == PostId);
        }

        public async Task AddBannerDesiner(long postId, Banner banner)
        {
            banner.DesinerId = postId;
            banner.Created_at = DateTime.Now;

            _context.Banner.Add(banner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateToDesiner(Banner banner, long postId)
        {
            var updateItem = await _context.Banner.FirstOrDefaultAsync(b => b.DesinerId == postId && b.Id == banner.Id);
            if (updateItem == null)
            {
                throw new Exception("Banner không tồn tại hoặc không liên kết với bài viết.");
            }

            updateItem.Name = banner.Name;
            updateItem.Meta_data = banner.Meta_data;
            updateItem.Updated_by = banner.Updated_by;
            updateItem.Updated_at = DateTime.Now;

            _context.Banner.Update(updateItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Banner> GetBannerByDesignerId(long PostId)
        {
            return await _context.Banner.Include(p => p.designertable).FirstOrDefaultAsync(p => p.DesinerId == PostId);
        }
    }
}
