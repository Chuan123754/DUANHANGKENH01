using appViews.Models;

namespace AppViews.IServices
{
    public interface SeoIServices
    {
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
