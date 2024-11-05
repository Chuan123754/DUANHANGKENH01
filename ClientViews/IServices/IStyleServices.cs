using ClientViews.Models;

namespace ClientViews.IServices
{
    public interface IStyleServices
    {
        Task<List<Style>> GetAll();
        Task<Style> Details(long id);
        Task Create(Style s);
        Task Update(Style s);
        Task Delete(long id);
        Task<List<Style>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}
