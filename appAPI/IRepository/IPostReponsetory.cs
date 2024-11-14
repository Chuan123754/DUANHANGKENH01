using appAPI.Models;
using appAPI.Models.DTO;

namespace appAPI.IRepository
{
    public interface IPostReponsetory
    {
        // Lấy tất cả bài viết theo type
        Task<List<Product_Posts>> GetAll();
        Task<List<Product_Posts>> GetAllByType(string type);

        // Lấy bài viết theo id và type
        Task<ModelPostTag> GetByIdAndType(long id, string type);
        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        Task<Product_Posts> CreatePost(Product_Posts post, List<long> tagIds, List<long> category);
        Task<Product_Posts> CreatePage(Product_Posts post, List<long> tagIds);
        Task<Product_Posts> CreateProduct(Product_Posts post, List<long> tagIds, List<long> category);
        Task<Product_Posts> CreateProject(Product_Posts post, List<long> tagIds, List<long> category);
        Task Update(Product_Posts post, List<long> tagIds);
        Task Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds);
        Task Delete(long id);
    }
}
