using appViews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppViews.IServices
{
    public interface IPostTagService
    {
        Task<List<Post_tags>> GetAll();
        Task<Post_tags> GetById(long id);
        Task Create(Post_tags postTag);
        Task Update(Post_tags postTag);
        Task Delete(long id);
    }
}
