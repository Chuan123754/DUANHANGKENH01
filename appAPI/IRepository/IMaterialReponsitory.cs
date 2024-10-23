using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IMaterialReponsitory
    {
        Task<List<Material>> GetAll();

        // Lấy bài viết theo id và type
        Task<Material> GetByIdAndType(long id);
        Task<List<Material>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
        Task Create(Material mate);
        Task Update(Material mate);
        Task Delete(long id);
    }
}
