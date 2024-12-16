using ViewsFE.Models;
using ViewsFE.Models.DTO;

namespace ViewsFE.IServices
{
    public interface IPostSer
    {
        Task<List<Product_Posts>> GetAllType(string type);
        Task<Product_Posts> GetByIdType(long id, string type);

        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        Task<int> GetTotalType(string type);
        Task<List<Product_Posts>> GetAllProductDelete();
        Task<List<Product_Posts>> GetByTypeAsyncDelete(string type, int pageNumber, int pageSize, string searchTerm);
        // Lấy tổng số bài viết theo type và tìm kiếm
        Task<int> GetTotalCountAsyncDelete(string type, string searchTerm);
        Task Restore(long id);
        Task<long> CreatePost(Product_Posts post, List<long> tagIds, List<long> category);
        Task<long> CreatePage(Product_Posts post, List<long> tagIds);
        Task<long> CreateProduct(Product_Posts post, List<long> tagIds, List<long> category);
        Task<long> CreateProject(Product_Posts post, List<long> tagIds, List<long> category);
        Task Delete(long id);
        Task<long> Update(Product_Posts post, List<long> tagIds);
        Task<long> Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds);
        Task<bool> CheckSlug(string slug);
    }
}
