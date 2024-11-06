using ClientViews.Models;

namespace ClientViews.IServices
{
    public interface ITagsServices
    {
        Task<List<Tags>> GetAll();
        Task<Tags> Details(long id);
        Task Create(Tags tag);
        Task Update(Tags tag);
        Task Delete(long id);
    }
}
