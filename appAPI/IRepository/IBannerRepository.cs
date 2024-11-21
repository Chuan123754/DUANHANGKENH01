using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IBannerRepository
    {
        Task<List<Banner>> GetAllBanner();
        Task<Banner> GetBannerById(long id);   
        Task<Banner> GetBannerByProductPostId(long PostId);
        Task Create(Banner banner);
        Task AddBannerToPost(long postId, Banner banner);
        Task Update(Banner banner , long postId);
        Task Delete(long id);
    }
}
