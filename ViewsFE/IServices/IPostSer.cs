using ViewsFE.Models;
using ViewsFE.Models.DTO;

namespace ViewsFE.IServices
{
    public interface IPostSer
    {
        Task<List<Product_Posts>> GetAllType(string type);
        Task<Product_Posts> GetByIdType(long id, string type);
        Task<ModelPostTag> GetByIdType_New(long id, string type);

        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        // Tạo bài viết
        Task CreatePost(Product_Posts post, List<long> tagIds, List<long> category);
        Task CreatePage(Product_Posts post, List<long> tagIds);
        Task CreateProduct(Product_Posts post, List<long> tagIds, List<long> category);
        Task CreateProject(Product_Posts post, List<long> tagIds, List<long> category);
        Task Delete(long id);
        Task Update(Product_Posts post, List<long> tagIds);
        Task Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds);

    }
}
