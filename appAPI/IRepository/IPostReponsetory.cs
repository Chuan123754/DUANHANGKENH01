using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IPostReponsetory
    {
        // Lấy tất cả bài viết theo type
        Task<List<Product_Posts>> GetAllByType(string type);

        // Lấy bài viết theo id và type
        Task<Product_Posts> GetByIdAndType(long id, string type);
        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        Task CreatePost(Product_Posts post);
        Task CreatePage(Product_Posts post);
        Task CreateProduct(Product_Posts post);
        Task CreateProject(Product_Posts post);
        Task Update(Product_Posts post);
        Task Delete(long id);
    }
}
