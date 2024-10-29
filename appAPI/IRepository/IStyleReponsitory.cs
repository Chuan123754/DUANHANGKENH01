using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IStyleReponsitory
    {
        Task<List<Style>> GetAll();
        Task<Style> GetByIdAndType(long id);
        Task<List<Style>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
        Task Create(Style style);
        Task Update(Style style);
        Task Delete(long id);
    }
}
