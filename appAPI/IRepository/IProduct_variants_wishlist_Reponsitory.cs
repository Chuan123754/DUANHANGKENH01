using appAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.IRepository
{
    public interface IProduct_variants_wishlist_Reponsitory
    {
        Task<List<ProductAttributes_wishlist>> GetAllPW();
        Task<ProductAttributes_wishlist> GetByID(long id);
        Task<string> Create(ProductAttributes_wishlist pw);
        Task<string> Delete(long id);
    }
}
