using appAPI.Models;

namespace appAPI.IRepository
{
    public interface SeoIReponsitory
    {
        Task<Seo> GetById(long id);
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
