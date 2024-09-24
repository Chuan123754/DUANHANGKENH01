using appAPI.Models;

namespace AppAPI.IRepository
{
    public interface SeoIReponsitory
    {
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
