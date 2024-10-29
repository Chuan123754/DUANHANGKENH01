using appAPI.Models;

namespace appAPI.IRepository
{
    public interface ISizeReponsitory
    {
        Task<List<Size>> GetAll();
        Task<Size> GetByIdAndType(long id);
        Task<List<Size>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
        Task Create(Size size);
        Task Update(Size size);
        Task Delete(long id);
    }
}
