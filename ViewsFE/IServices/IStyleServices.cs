using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IStyleServices
    {
        Task<List<Style>> GetAll();
        Task<Style> Details(long id);
        Task Create(Style s);
        Task Update(Style s);
        Task Delete(long id);
        Task<List<Style>> Search(string keyword);
    }
}
