using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface SeoIServices
    {
        Task<Seo> GetById(long id);
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
