using appAPI.Models;

namespace appAPI.IRepository
{
    public interface ITagsRepository
    {
        Task<List<Tags>> GetAll();
        Task<Tags> Details(long id);
        Task Create(Tags tag);
        Task Update(Tags tag);
        Task Delete(long id);
    }
}
