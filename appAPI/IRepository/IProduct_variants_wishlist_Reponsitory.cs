using appAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.IRepository
{
    public interface IProduct_variants_wishlist_Reponsitory
    {
        Task<List<Product_variants_wishlist>> GetAllPW();
        Task<Product_variants_wishlist> GetByID(long id);
        Task<string> Create(Product_variants_wishlist pw);
        Task<string> Delete(long id);
    }
}
