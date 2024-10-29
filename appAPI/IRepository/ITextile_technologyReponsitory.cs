using appAPI.Models;

namespace appAPI.IRepository
{
    public interface ITextile_technologyReponsitory
    {
        Task<List<Textile_technology>> GetAll();
        // Lấy bài viết theo id và type
        Task<Textile_technology> GetByIdAndType(long id);
        Task<List<Textile_technology>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
        Task Create(Textile_technology texti);
        Task Update(Textile_technology texti);
        Task Delete(long id);
    }
}
