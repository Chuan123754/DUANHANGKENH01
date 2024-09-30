using ViewsFE.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewsFE.IServices
{
    public interface IPostService
    {
        Task<List<Posts>> GetAll();
        Task<Posts> GetById(long id);
        Task<List<Categories>> GetCategoriesByPostId(long postId);
        Task<List<Tags>> GetTagsByPostId(long postId);
        Task<List<Posts>> SearchPosts(string keyword);
        Task Create(Posts post);
        Task Update(Posts post);
        Task Delete(long id);
        Task<long> UploadFile(IFormFile file);
    }
}
