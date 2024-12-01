using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IContactReponsetory
    {
        Task<List<Contact>> GetAll();
        Task<Contact> GetById(long id);
        Task Create(Contact c);
        Task Delete(long id);
        Task<List<Contact>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}
