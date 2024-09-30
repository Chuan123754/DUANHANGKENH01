using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface SeoIServices
    {
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
