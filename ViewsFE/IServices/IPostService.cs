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
        Task Create(Posts post, IFormFile imageFil);
        Task Update(Posts post);
        Task Delete(long id);
        Task<long> UploadFile(IFormFile file);
    }
}
