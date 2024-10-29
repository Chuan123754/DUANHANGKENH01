using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IWishlistReponsitory
    {
        Task<List<Wishlist>> GetAll();
        Task<Wishlist> GetByIdAndType(long id);
        Task<List<Wishlist>> GetByTypeAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task Create(Wishlist list);
        Task Delete(long id);
    }
}
