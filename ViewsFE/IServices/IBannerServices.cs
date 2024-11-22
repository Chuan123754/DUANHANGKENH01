using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IBannerServices
    {
        Task<List<Banner>> GetAllBanner();
        Task<Banner> GetBannerById(long id);
        Task<Banner> GetBannerByProductPostId(long PostId);
        Task<Banner> GetBannerByDesignerId(long PostId);
        Task Create(Banner banner);
        Task AddBannerToPost(long postId, Banner banner);
        Task Update(Banner banner, long postId);
        Task AddBannerDesiner(long postId, Banner banner);
        Task UpdateToDesiner(Banner banner, long postId);
        Task Delete(long id);
    }
}
