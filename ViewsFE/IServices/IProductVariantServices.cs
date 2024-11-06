using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IProductVariantServices
    {
        Task<List<Product_variants>> GetAllProductVarians();
        Task<Product_variants> GetProductVariantsById(long id);
        Task<long> Create (Product_variants productVariants);
        Task Update(Product_variants productVariants, long id);
        Task Delete(long id);
    }
}
