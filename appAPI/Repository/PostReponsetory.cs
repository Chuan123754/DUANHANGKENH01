using appAPI.IRepository;
using appAPI.Models;
using appAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace appAPI.Repository
{
    public class PostReponsetory : IPostReponsetory
    {
        private readonly APP_DATA_DATN _context;

        public PostReponsetory(APP_DATA_DATN context)
        {
            _context = context;
        }

        // Hàm tạo bài viết chung cho nhiều loại (post, page, product, project)
        private async Task<Product_Posts> CreatePostInternal(Product_Posts post, List<long> tagIds, List<long> categoryIds, string type)
        {
            post.Type = type;
            post.Deleted = false;
            post.Created_at = DateTime.Now;
            post.Deleted_at = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            await AddTags(post.Id, tagIds);
            await AddCategories(post.Id, categoryIds);
            return post;
        }

        // Hàm thêm tags vào bài viết
        private async Task AddTags(long postId, List<long> tagIds)
        {
            var existingTags = await _context.Post_Tags
                .Where(pt => pt.Post_Id == postId)
                .ToListAsync();

            _context.Post_Tags.RemoveRange(existingTags);
            await _context.SaveChangesAsync();

            if (tagIds.Any())
            {
                var newTags = tagIds.Select(tagId => new Post_tags
                {
                    Post_Id = postId,
                    Tag_Id = tagId
                });

                await _context.Post_Tags.AddRangeAsync(newTags);
                await _context.SaveChangesAsync();
            }
        }


        // Hàm thêm categories vào bài viết
        private async Task AddCategories(long postId, List<long> categoryIds)
        {
            var existingCategories = await _context.Post_Categories
                .Where(pc => pc.Post_Id == postId)
                .ToListAsync();

            _context.Post_Categories.RemoveRange(existingCategories);
            await _context.SaveChangesAsync();

            if (categoryIds.Any())
            {
                var newCategories = categoryIds.Select(categoryId => new Post_categories
                {
                    Post_Id = postId,
                    Category_Id = categoryId
                });

                await _context.Post_Categories.AddRangeAsync(newCategories);
                await _context.SaveChangesAsync();
            }
        }

        // Hàm tạo bài viết loại post
        public async Task<Product_Posts> CreatePost(Product_Posts post, List<long> tagIds, List<long> categoryIds)
        {
            return await CreatePostInternal(post, tagIds, categoryIds, "post");
        }

        // Hàm tạo bài viết loại page
        public async Task<Product_Posts> CreatePage(Product_Posts post, List<long> tagIds)
        {
            return await CreatePostInternal(post, tagIds, new List<long>(), "page");
        }

        // Hàm tạo bài viết loại product
        public async Task<Product_Posts> CreateProduct(Product_Posts post, List<long> tagIds, List<long> categoryIds)
        {
            return await CreatePostInternal(post, tagIds, categoryIds, "product");
        }

        // Hàm tạo bài viết loại project
        public async Task<Product_Posts> CreateProject(Product_Posts post, List<long> tagIds, List<long> categoryIds)
        {
            return await CreatePostInternal(post, tagIds, categoryIds, "project");
        }

        // Hàm xóa bài viết
        public async Task Delete(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                post.Deleted = true;
                post.Deleted_at = DateTime.Now;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy tất cả bài viết theo loại
        public async Task<List<Product_Posts>> GetAllByType(string type)
        {
            return await _context.Posts
                .Where(p => p.Type == type && p.Deleted == false)
                .Include(p => p.Post_tags)          // Lấy thông tin về Post_tags
                    .ThenInclude(pt => pt.Tag)      // Lấy thông tin chi tiết của Tag liên quan
                .Include(p => p.Post_categories)    // Lấy thông tin về Post_categories
                    .ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)           // Lấy thông tin Designer
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }


        // Lấy tất cả bài viết
        public async Task<List<Product_Posts>> GetAll()
        {
            return await _context.Posts
                .Where(p => p.Deleted == false)
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        // Lấy bài viết theo ID và loại
        public async Task<Product_Posts> GetByIdAndType(long id, string type)
        {
            return await _context.Posts
                .Where(p => p.Id == id && p.Type == type && p.Deleted == false)
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();
         
        }

        // Lấy bài viết theo loại với phân trang và tìm kiếm
        public async Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string? searchTerm)
        {
            return await _context.Posts
                .Where(p => p.Type == type && (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)) && p.Deleted == false)
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
         
        }

        // Lấy tổng số bài viết theo loại và tìm kiếm
        public async Task<int> GetTotalCountAsync(string type, string? searchTerm)
        {
            return await _context.Posts
                .CountAsync(p => p.Type == type && p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        // Cập nhật bài viết
        public async Task<Product_Posts> Update(Product_Posts post, List<long> tagIds)
        {
            var item = await _context.Posts.FindAsync(post.Id);
            // Cập nhật thông tin bài viết
            item.Title = post.Title;
            item.Slug = post.Slug;
            item.Status = post.Status;
            item.AuthorId = post.AuthorId;
            item.Image_library = post.Image_library;
            item.Feature_image = post.Feature_image;
            item.Description = post.Description;
            item.Short_description = post.Short_description;
            item.Updated_by = post.Updated_by;
            item.Updated_at = DateTime.Now;
            item.Post_date = DateTime.Now;

            _context.Posts.Update(item);
            await _context.SaveChangesAsync();

            // Cập nhật tags
            await AddTags(post.Id, tagIds ?? new List<long>()); // Xử lý null hoặc danh sách rỗng
            return item;
        }

        // Cập nhật tag và category
        public async Task<Product_Posts> Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds)
        {
            var item = await _context.Posts.FindAsync(post.Id);
            if (item == null) return null;

            // Cập nhật thông tin bài viết
            item.Title = post.Title;
            item.Slug = post.Slug;
            item.Status = post.Status;
            item.AuthorId = post.AuthorId;
            item.Image_library = post.Image_library;
            item.Feature_image = post.Feature_image;
            item.Description = post.Description;
            item.Short_description = post.Short_description;
            item.Updated_by = post.Updated_by;
            item.Updated_at = DateTime.Now;
            item.Post_date = DateTime.Now;

            _context.Posts.Update(item);
            await _context.SaveChangesAsync();

            // Xử lý tags và categories
            await AddTags(post.Id, tagIds ?? new List<long>()); // Xử lý null hoặc danh sách rỗng
            await AddCategories(post.Id, categoryIds ?? new List<long>()); // Xử lý null hoặc danh sách rỗng
            return item;
        }
    }
}
