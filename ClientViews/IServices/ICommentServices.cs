using ClientViews.Models;

namespace ClientViews.IServices
{
    public interface ICommentServices
    {
        Task<List<Comments>> GetAll();
        Task<Comments> GetById(long id);
        Task Create(Comments comments);
        Task Update(long id, Comments comments);
        Task Delete(long id);
    }
}
