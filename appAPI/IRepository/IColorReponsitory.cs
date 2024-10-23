using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IColorReponsitory
    {
        Task<List<Color>> GetAll();
        Task<Color> GetByIdAndType(long id);
        Task<List<Color>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
        Task Create(Color color);
        Task Update(Color color);
        Task Delete(long id);
    }
}
