using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IOptionsRepository
    {
        Task<List<Options>> GetAll();
        Task<Options> Details(long id);
        Task Create(Options op);
        Task Update(Options op);
        Task Delete(long id);
    }
}
