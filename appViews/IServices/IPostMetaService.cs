using appViews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppViews.IServices
{
    public interface IPostMetaService
    {
        Task<List<Post_metas>> GetAll();
        Task<Post_metas> GetById(long id);
        Task Create(Post_metas postMeta);
        Task Update(Post_metas postMeta);
        Task Delete(long id);
    }
}
