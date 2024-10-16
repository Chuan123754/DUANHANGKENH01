//using ViewsFE.Models;
//using Microsoft.AspNetCore.Http;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace ViewsFE.IServices
//{
//    public interface IPostSer
//    {
//        Task<List<Product_Posts>> GetAll();
//        Task<Product_Posts> GetById(long id);
//        Task<List<Categories>> GetCategoriesByPostId(long postId);
//        Task<List<Tags>> GetTagsByPostId(long postId);
//        Task<List<Product_Posts>> SearchPosts(string keyword);
//        Task Create(Product_Posts post);
//        Task Update(Product_Posts post);
//        Task Delete(long id);
//        Task<long> UploadFile(IFormFile file);
//    }
//}
