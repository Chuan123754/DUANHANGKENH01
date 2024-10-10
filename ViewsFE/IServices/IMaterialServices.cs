using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IMaterialServices
    {
        Task<List<Material>> GetAll();
        Task<Material> Details(long id);
        Task Create(Material s);
        Task Update(Material s);
        Task Delete(long id);
        Task<List<Material>> Search(string keyword);
    }
}
