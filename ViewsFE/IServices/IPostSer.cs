using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IPostSer
    {
        // Lấy tất cả bài viết theo type
        Task<List<Product_Posts>> GetAllProduct();
        Task<List<Product_Posts>> GetAllPost();
        Task<List<Product_Posts>> GetAllPage();

        // Lấy bài viết theo id và type
        Task<Product_Posts> GetByIdProduct(long id);
        Task<Product_Posts> GetByIdPost(long id);
        Task<Product_Posts> GetByIdPage(long id);

        // Lấy bài viết theo type với phân trang và tìm kiếm
        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);

        // Tạo bài viết
        Task CreatePost(Product_Posts post);
        Task CreatePage(Product_Posts post);
        Task CreateProduct(Product_Posts post);

        // Cập nhật bài viết
        Task Update(Product_Posts post);

        // Xóa bài viết
        Task Delete(long id);
    }

}
