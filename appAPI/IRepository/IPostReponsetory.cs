using appAPI.Models;
using appAPI.Models.DTO;

namespace appAPI.IRepository
{
    public interface IPostReponsetory
    {
        // Lấy tất cả bài viết
        Task<List<Product_Posts>> GetAll();

        // Lấy tất cả bài viết theo type
        Task<List<Product_Posts>> GetAllByType(string type);

        // Lấy bài viết theo id và type
        Task<Product_Posts> GetByIdAndType(long id, string type);

        // Lấy bài viết theo type với phân trang và tìm kiếm
        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);

        // Lấy tổng số bài viết theo type và tìm kiếm
        Task<int> GetTotalCountAsync(string type, string searchTerm);

        // Tạo bài viết loại post
        Task<Product_Posts> CreatePost(Product_Posts post, List<long> tagIds, List<long> categoryIds);

        // Tạo bài viết loại page
        Task<Product_Posts> CreatePage(Product_Posts post, List<long> tagIds);

        // Tạo bài viết loại product
        Task<Product_Posts> CreateProduct(Product_Posts post, List<long> tagIds, List<long> categoryIds);

        // Tạo bài viết loại project
        Task<Product_Posts> CreateProject(Product_Posts post, List<long> tagIds, List<long> categoryIds);

        // Cập nhật bài viết
        Task<Product_Posts> Update(Product_Posts post, List<long> tagIds);

        // Cập nhật tag và category cho bài viết
        Task<Product_Posts> Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds);

        // Xóa bài viết
        Task Delete(long id);
    }
}
