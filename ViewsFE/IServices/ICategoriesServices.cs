using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface ICategoriesServices
    {
        Task<List<Categories>> GetAll();
        Task<Categories> Details(long id);
        Task Create(Categories c);
        Task Update(Categories c);
        Task Delete(long id);
        Task<List<Categories>> Search(string keyword);
    }
}
