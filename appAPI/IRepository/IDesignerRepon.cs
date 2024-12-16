using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IDesignerRepon
    {
        Task<List<Designer>> GetAll();
        Task<List<Designer>> GetAllAC();
        Task<Designer> GetById(long id);
        Task<Designer> Create(Designer at);
        Task<Designer> Update(Designer at);
        Task Delete(long id);
        Task<List<Designer>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
        Task<List<Designer>> GetByTypeAsyncClient(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsyncClient(string searchTerm);
    }
}
