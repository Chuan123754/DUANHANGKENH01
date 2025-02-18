﻿using appAPI.Models;
using appAPI.Models.DTO;

namespace appAPI.IRepository
{
    public interface IPostReponsetory
    {
        // Lấy tất cả bài viết
        Task<List<Product_Posts>> GetAll();

        // Lấy tất cả bài viết theo type
        Task<List<Product_Posts>> GetAllByType(string type);
        Task<List<Product_Posts>> GetAllByClientTypeCate(string type, string cate);
        Task<List<Product_Posts>> GetCountByType(string type, long designerId);
        Task<List<Product_Posts>> GetCountByTypeDesigner(long designerId);
        Task<List<Product_Posts>> GetAllProductDelete();
        Task Restore(long id);
        Task<string?> GetNameDesigner(long id);
        // Lấy bài viết theo id và type
        Task<Product_Posts> GetByIdAndType(long id, string type);
        Task<Product_Posts> GetBySlugAndType(string slug, string type);
        Task<bool> CheckSlugForUpdate(string slug, long postId);

        // Lấy bài viết theo type với phân trang và tìm kiếm
        Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string searchTerm);
        // Lấy tổng số bài viết theo type và tìm kiếm
        Task<int> GetTotalCountAsync(string type, string searchTerm);
        Task<int> GetTotalType(string type);
        Task<List<Product_Posts>> GetByTypeAsyncProduct(string type, int pageNumber, int pageSize, string searchTerm);
        // Lấy tổng số bài viết theo type và tìm kiếm
        Task<int> GetTotalCountAsyncProduct(string type, string searchTerm);
        Task<List<Product_Posts>> GetByTypeAsyncDelete(string type, int pageNumber, int pageSize, string searchTerm);
        // Lấy tổng số bài viết theo type và tìm kiếm
        Task<int> GetTotalCountAsyncDelete(string type, string searchTerm);
        //Phân trang theo type và id cate
        Task<List<Product_Posts>> GetByTypeAsyncCate(string type, long categoryId, int pageNumber, int pageSize);

        // Lấy tổng số bài viết theo type và id cate
        Task<int> GetTotalCountAsyncCate(string type, long categoryId);

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
        Task<List<Product_Posts>> GetAllByClient();
        Task<List<Product_Posts>> GetByTypeAsyncFilter(List<long?> idDesigner, List<long?> idColor, List<long?> idMaterial, List<long?> idTextile_technology, List<long?> idStyle, List<long?> idSize,List<long?> idCategory, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsyncFilter(List<long?> idDesigner, List<long?> idColor, List<long?> idMaterial, List<long?> idTextile_technology, List<long?> idStyle, List<long?> idSize, List<long?> idCategory, string searchTerm);
        Task<List<Product_Posts>> GetByTypeAsyncFilter2(string type, List<long?> idDesigner, List<long?> idCategory, int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsyncFilter2(string type, List<long?> idDesigner, List<long?> idCategory, string searchTerm);
    }
}
