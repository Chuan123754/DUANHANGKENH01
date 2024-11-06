using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IBannerRepository
    {
        Task<List<Banner>> GetAllBanner();
        Task<Banner> GetBannerById(long id);    
        Task Create(Banner banner);
        Task Update(Banner banner , long id);
        Task Delete(long id);
    }
}
