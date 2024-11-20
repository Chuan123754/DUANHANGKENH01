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
            if (banner.Id > 0)
            {
                // Lấy banner từ bảng Banner
                var bannerItem = await _context.Banner.FindAsync(banner.Id);

                // Kiểm tra nếu không tìm thấy bannerItem
                if (bannerItem == null)
                {
                    // Trả về lỗi hoặc xử lý nếu không tìm thấy banner
                    throw new Exception("Banner not found.");
                }

                // Cập nhật ProductPostId trong Banner để liên kết với bài viết
                bannerItem.ProductPostId = postId;
                bannerItem.Created_at = DateTime.Now;

                // Thực hiện lưu thay đổi
                _context.Banner.Add(bannerItem);  // Thay _context.Banner.Add() bằng Update()
                await _context.SaveChangesAsync();
            }
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
                updateItem.Meta_data = banner.Meta_data;
                updateItem.Updated_by = banner.Updated_by;
                updateItem.Updated_at=DateTime.Now;

                _context.Banner.Update(updateItem);
                await _context.SaveChangesAsync();
            }

        }
    }
}
