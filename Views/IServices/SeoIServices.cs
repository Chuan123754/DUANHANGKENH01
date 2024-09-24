using Views.Models;

namespace Views.IServices
{
    public interface SeoIServices
    {
        Task CreateSeo(Seo seo);
        Task UpdateSeo(Seo seo);
    }
}
