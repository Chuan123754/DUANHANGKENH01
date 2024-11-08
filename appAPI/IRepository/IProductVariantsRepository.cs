using appAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.IRepository
{
    public interface IProductVariantsRepository
    {
        Task<List<Product_variants>> GetAllProductVarians();
        Task<Product_variants> GetProductVariantsById(long id);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        Task Create(Product_variants productVariants);
        Task Update(Product_variants productVariants , long id);
        Task Delete(long id);      
        
    }
}
