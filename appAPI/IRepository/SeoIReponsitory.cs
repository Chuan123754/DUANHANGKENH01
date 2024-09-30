using appAPI.Models;

namespace appAPI.IRepository
{
    public interface SeoIReponsitory
    {
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
