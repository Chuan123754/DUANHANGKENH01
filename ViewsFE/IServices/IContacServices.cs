using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IContacServices
    {
        Task<List<Contact>> GetAll();
        Task<Contact> GetById(long id);
        Task Create(Contact c);
        Task Delete(long id);
        Task<List<Contact>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}
