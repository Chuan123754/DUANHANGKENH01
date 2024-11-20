using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IBannerServices
    {
        Task<List<Banner>> GetAllBanner();
        Task<Banner> GetBannerById(long id);
        Task Create(Banner banner);
        Task AddBannerToPost(long postId, Banner banner);
        Task Update(Banner banner, long id);
        Task Delete(long id);
    }
}
