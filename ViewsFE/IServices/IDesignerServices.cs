using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IDesignerServices
    {
        Task<List<Designer>> GetAll();
        Task<Designer> GetById(long id);
        Task<long> Create(Designer at);
        Task<long> Update(Designer at);
        Task Delete(long id);
        Task<List<Designer>> GetByTypeAsync( int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync( string searchTerm);
    }
}
