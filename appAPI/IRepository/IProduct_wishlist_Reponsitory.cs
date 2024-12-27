using appAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.IRepository
{
    public interface IProduct_wishlist_Reponsitory
    {
        Task<List<Product_wishlist>> GetAllPW();
        Task<Product_wishlist> GetByID(long id);
        Task<string> Create(Product_wishlist pw);
        Task<string> Delete(long id);
    }
}
