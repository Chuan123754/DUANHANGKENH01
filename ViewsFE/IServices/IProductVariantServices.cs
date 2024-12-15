using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IProductVariantServices
    {
        Task<List<Product_variants>> GetAllProductVarians();
        Task<Product_variants> GetProductVariantsById(long id);
        Task<List<Product_Attributes>> GetProductAttributesByProductVarianId(long productVariantId);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        Task<List<Product_Posts>> GetAllProductPosts();
        Task<long> Create (Product_variants productVariants);
        Task Update(Product_variants productVariants, long id);
        Task Delete(long id);
        Task<int> GetTotal();

        Task<Product_variants?> FindVariant(long postId, byte textileTechnologyId, byte styleId, byte materialId);

    }
}
