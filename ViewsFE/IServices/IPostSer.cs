using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IPostSer
    {
        // Lấy tất cả bài viết theo type
        Task<List<Product_Posts>> GetAllProduct();
        Task<List<Product_Posts>> GetAllPost();
        Task<List<Product_Posts>> GetAllPage();
        Task<List<Product_Posts>> GetAllProject();

        // Lấy bài viết theo id và type
        Task<Product_Posts> GetByIdProduct(long id);
        Task<Product_Posts> GetByIdPost(long id);
        Task<Product_Posts> GetByIdPage(long id);
        Task<Product_Posts> GetByIdProject(long id);
        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        // Tạo bài viết
        Task CreatePost(Product_Posts post);
        Task CreatePage(Product_Posts post);
        Task CreateProduct(Product_Posts post);
        Task CreateProject(Product_Posts post);
        Task Update(Product_Posts post);
        Task Delete(long id);
    }

}
