using appAPI.Models;

namespace AppAPI.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetAll();
        Task<Comments> GetById(long id);
        Task<Comments> Create( Comments comments);
        Task<Comments> Update(long id, Comments comments);
        Task<Comments> Delete(long id);
    }
}
