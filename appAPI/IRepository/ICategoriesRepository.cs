using appAPI.Models;

namespace AppAPI.IRepository
{
    public interface ICategoriesRepository
    {
        Task<List<Categories>> GetAll();
        Task<Categories> Details(long id);
        Task Create(Categories c);
        Task Update(Categories c);
        Task Delete(long id);
        Task<List<Categories>> Search(string keyword);
    }
}
