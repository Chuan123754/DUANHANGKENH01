using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IProductVariantsRepository
    {
        Task<List<Product_variants>> GetAllProductVarians();
        Task<Product_variants> GetProductVariantsById(long id);
        Task Create(Product_variants productVariants);
        Task Update(Product_variants productVariants , long id);
        Task Delete(long id);      
        
    }
}
