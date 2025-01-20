using appAPI.IRepository;
using appAPI.Models;
using appAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
            post.Created_at = DateTime.Now;
            post.Post_date = DateTime.Now;

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
                post.Status = "delete";
                post.Deleted_at = DateTime.Now;
                _context.Posts.Update(post);

                var lst_Product = _context.Product_Attributes.Where(pa => pa.Post_Id == post.Id).ToList();
                foreach(var item in lst_Product)
                {
                    item.Status = "Đã xoá";
                    _context.Product_Attributes.Update(item);
                    await _context.SaveChangesAsync();
                }   
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Product_Posts>> GetAllProductDelete()
        {
            return await _context.Posts.Where(p => p.Type == "product" && p.Status == "delete")
                .Include(p => p.Post_tags)          // Lấy thông tin về Post_tags
                    .ThenInclude(pt => pt.Tag)      // Lấy thông tin chi tiết của Tag liên quan
                .Include(p => p.Post_categories)    // Lấy thông tin về Post_categories
                    .ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)           // Lấy thông tin Designer
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task Restore(long id)
        {
            // Tìm bài viết dựa trên ID
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                throw new Exception("Bài viết không tồn tại.");
            }

            // Kiểm tra xem đường dẫn đã tồn tại hay chưa
            bool isSlugExist = await _context.Posts
                .AnyAsync(p => p.Slug == post.Slug && p.Id != id && p.Status != "delete");

            if (isSlugExist)
            {
                // Trả lỗi chi tiết
                throw new Exception("Đường dẫn bài viết đã tồn tại.");
            }

            // Khôi phục bài viết
            post.Status = "publish";
            post.Deleted_at = null;
            // Lưu thay đổi
            _context.Posts.Update(post);
            var lst_Product = _context.Product_Attributes.Where(pa => pa.Post_Id == post.Id).ToList();
            foreach (var item in lst_Product)
            {
                item.Status = "Còn hàng";
                _context.Product_Attributes.Update(item);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }


        // Lấy tất cả bài viết theo loại
        public async Task<List<Product_Posts>> GetAllByType(string type)
        {
            return await _context.Posts
                .Where(p => p.Type == type && p.Status == "publish")
                .Include(p => p.Post_tags)          // Lấy thông tin về Post_tags
                    .ThenInclude(pt => pt.Tag)      // Lấy thông tin chi tiết của Tag liên quan
                .Include(p => p.Post_categories)    // Lấy thông tin về Post_categories
                    .ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)           // Lấy thông tin Designer
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<List<Product_Posts>> GetAllByClientTypeCate(string type, string cate)
        {
            return await _context.Posts
                .Where(p => p.Type == type && p.Status == "publish" && p.Post_categories.Any(pc => pc.Categories.Slug == cate))
                .Include(p => p.Post_tags)          // Lấy thông tin về Post_tags
                    .ThenInclude(pt => pt.Tag)      // Lấy thông tin chi tiết của Tag liên quan
                .Include(p => p.Post_categories)    // Lấy thông tin về Post_categories
                    .ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)           // Lấy thông tin Designer
                .OrderBy(p => p.STT == null)
                .ThenBy(p => p.STT)
                .ToListAsync();
        }
        // Lấy tất cả bài viết
        public async Task<List<Product_Posts>> GetAll()
        {
            return await _context.Posts
                .Where(p => p.Status != "delete")
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
        public async Task<List<Product_Posts>> GetAllByClient()
        {
            return await _context.Posts
                .Where(p => p.Status == "publish" && p.Type == "product")
                .Include(pc => pc.Post_categories).ThenInclude(pc => pc.Categories) // Bao gồm Categories từ Post_categories
                .Include(pt => pt.Post_tags).ThenInclude(pt => pt.Tag) // Bao gồm Tags từ Post_tags
                .Include(pd => pd.Designer) // Bao gồm Designer từ Posts
                .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Size) // Bao gồm Size từ Product_Attributes
                .Include(p => p.Product_Attributes).ThenInclude(pl => pl.Color) // Bao gồm Color từ Product_Attributes

                .Include(p => p.Product_Attributes).ThenInclude(pm => pm.Material) // Bao gồm Material
                .Include(p => p.Product_Attributes).ThenInclude(pt => pt.Textile_Technology) // Bao gồm Textile_Technology
                .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Style) // Bao gồm Style
                .OrderByDescending(p => p.Created_at) // Sắp xếp giảm dần theo Created_at
                 .Select(p => new Product_Posts
                 {
                     Id = p.Id,
                     STT = p.STT,
                     Title = p.Title,
                     Slug = p.Slug,
                     Status = p.Status,
                     AuthorId = p.Designer.id_Designer,
                     Type = p.Type,
                     Short_description = p.Short_description,
                     Description = p.Description,
                     Image_library = p.Image_library,
                     Feature_image = p.Feature_image,
                     Post_date = p.Post_date,
                     Created_at = p.Created_at,
                     Updated_at = p.Updated_at,
                     Deleted_at = p.Deleted_at,
                     Product_Attributes = p.Product_Attributes, // Bao gồm thuộc tính
                     MinPrice = p.Product_Attributes.Min(pa => pa.Sale_price ?? pa.Regular_price), // Giá thấp nhất
                     MaxPrice = p.Product_Attributes.Max(pa => pa.Sale_price ?? pa.Regular_price)  // Giá cao nhất
                 })
                .ToListAsync();
        }

        // Lấy bài viết theo ID và loại
        public async Task<Product_Posts> GetByIdAndType(long id, string type)
        {
            return await _context.Posts
                .Where(p => p.Id == id && p.Type == type)
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();
        }

        // Lấy bài viết theo loại với phân trang và tìm kiếm
        public async Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string? searchTerm)
        {
            return await _context.Posts
                .Where(p => p.Type == type && (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)) && p.Status != "delete")
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
                .CountAsync(p => p.Type == type && p.Status != "delete" &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }
        public async Task<int> GetTotalType(string type)
        {
            return await _context.Posts
                .CountAsync(p => p.Type == type && p.Status != "delete");
        }

        public async Task<List<Product_Posts>> GetByTypeAsyncProduct(string type, int pageNumber, int pageSize, string? searchTerm)
        {
            return await _context.Posts
             .Where(p => p.Status == "publish")
             .Include(pc => pc.Post_categories).ThenInclude(pc => pc.Categories) // Bao gồm Categories từ Post_categories
             .Include(pt => pt.Post_tags).ThenInclude(pt => pt.Tag) // Bao gồm Tags từ Post_tags
             .Include(pd => pd.Designer) // Bao gồm Designer từ Posts
             .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Size) // Bao gồm Size từ Product_Attributes
             .Include(p => p.Product_Attributes).ThenInclude(pl => pl.Color) // Bao gồm Color từ Product_Attributes
             .Include(p => p.Product_Attributes).ThenInclude(pm => pm.Material) // Bao gồm Material
             .Include(p => p.Product_Attributes).ThenInclude(pt => pt.Textile_Technology) // Bao gồm Textile_Technology
             .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Style) // Bao gồm Style
             .OrderByDescending(p => p.Created_at) // Sắp xếp giảm dần theo Created_at
              .Select(p => new Product_Posts
              {
                  Id = p.Id,
                  STT = p.STT,
                  Title = p.Title,
                  Slug = p.Slug,
                  Status = p.Status,
                  AuthorId = p.Designer.id_Designer,
                  Type = p.Type,
                  Short_description = p.Short_description,
                  Description = p.Description,
                  Image_library = p.Image_library,
                  Feature_image = p.Feature_image,
                  Post_date = p.Post_date,
                  Created_at = p.Created_at,
                  Updated_at = p.Updated_at,
                  Deleted_at = p.Deleted_at,
                  Product_Attributes = p.Product_Attributes, // Bao gồm thuộc tính
                  MinPrice = p.Product_Attributes.Min(pa => pa.Sale_price ?? pa.Regular_price), // Giá thấp nhất
                  MaxPrice = p.Product_Attributes.Max(pa => pa.Sale_price ?? pa.Regular_price)  // Giá cao nhất
              })
             .ToListAsync();
        }

        public async Task<int> GetTotalCountAsyncProduct(string type, string searchTerm)
        {
            return await _context.Posts
                  .CountAsync(p => p.Type == type && p.Status == "publish" &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }
        public async Task<List<Product_Posts>> GetByTypeAsyncDelete(string type, int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Posts
                .Where(p => p.Type == type && p.Status == "delete" && (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)))
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsyncDelete(string type, string searchTerm)
        {
            return await _context.Posts
                .CountAsync(p => p.Type == type && p.Status == "delete" &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }
        public async Task<List<Product_Posts>> GetByTypeAsyncCate(string type, long categoryId, int pageNumber, int pageSize)
        {
            return await _context.Posts
                .Where(post => post.Type == type && post.Status != "delete")
                .Where(post => post.Post_categories.Any(pc => pc.Category_Id == categoryId))
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)
                .OrderByDescending(post => post.Created_at)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bài viết của trang trước
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsyncCate(string type, long categoryId)
        {
            return await _context.Posts
                 .CountAsync(p => p.Type == type && p.Status != "delete" && p.Post_categories.Any(pc => pc.Category_Id == categoryId));
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
            item.Updated_at = DateTime.Now;

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
            item.STT = post.STT;
            item.Updated_at = DateTime.Now;
            item.Post_date = DateTime.Now;

            _context.Posts.Update(item);
            await _context.SaveChangesAsync();

            // Xử lý tags và categories
            await AddTags(post.Id, tagIds ?? new List<long>()); // Xử lý null hoặc danh sách rỗng
            await AddCategories(post.Id, categoryIds ?? new List<long>()); // Xử lý null hoặc danh sách rỗng
            return item;
        }

        public async Task<List<Product_Posts>> GetCountByType(string type, long designerId)
        {
            return await _context.Posts
                  .Where(p => p.Type == type && p.Status != "delete" && p.AuthorId == designerId)
                  .Include(p => p.Post_tags)          // Lấy thông tin về Post_tags
                      .ThenInclude(pt => pt.Tag)      // Lấy thông tin chi tiết của Tag liên quan
                  .Include(p => p.Post_categories)    // Lấy thông tin về Post_categories
                      .ThenInclude(pc => pc.Categories)
                  .Include(p => p.Designer)           // Lấy thông tin Designer
                  .OrderByDescending(p => p.Post_date)
                  .Take(4)
                  .ToListAsync();
        }

        public async Task<string?> GetNameDesigner(long id)
        {
            return await _context.Designer
                                 .Where(p => p.id_Designer == id)
                                 .Select(p => p.Name)
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<Product_Posts>> GetCountByTypeDesigner(long designerId)
        {
            return await _context.Posts
                .Where(p => p.AuthorId == designerId && p.Status == "publish")
                  .Include(pc => pc.Post_categories) // Bao gồm bảng Post_categories từ Posts
                          .ThenInclude(pc => pc.Categories) // Bao gồm Categories từ Post_categories
                  .Include(pt => pt.Post_tags) // Bao gồm bảng Post_tags từ Posts
                          .ThenInclude(pt => pt.Tag) // Bao gồm Tags từ Post_tags
                  .Include(pd => pd.Designer) // Bao gồm Designer từ Posts
                  .Include(p => p.Product_Attributes).ThenInclude(pm => pm.Material) // Bao gồm Material
                  .Include(p => p.Product_Attributes).ThenInclude(pt => pt.Textile_Technology) // Bao gồm Textile_Technology
                  .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Style) // Bao gồm Style
                  .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Size) // Bao gồm Style
                  .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Color) // Bao gồm Style
                  .OrderByDescending(p => p.Created_at) // Sắp xếp giảm dần theo Created_at                 
                  .ToListAsync();
        }

        public async Task<List<Product_Posts>> GetByTypeAsyncFilter(List<long?> idDesigner, List<long?> idColor, List<long?> idMaterial, List<long?> idTextile_technology, List<long?> idStyle, List<long?> idSize, List<long?> idCategory, int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Posts
                        .Where(p => p.Type == "Product" &&
                        (idDesigner == null || !idDesigner.Any() || idDesigner.Contains(p.AuthorId)) && // Lọc theo danh sách tác giả
                        (idTextile_technology == null || !idTextile_technology.Any() || p.Product_Attributes.Any(a => idTextile_technology.Contains(a.Textile_technology_id))) &&
                        (idStyle == null || !idStyle.Any() || p.Product_Attributes.Any(a => idStyle.Contains(a.Style_id))) &&
                        (idMaterial == null || !idMaterial.Any() || p.Product_Attributes.Any(a => idMaterial.Contains(a.Material_id))) &&
                        (idSize == null || !idSize.Any() || p.Product_Attributes.Any(a => idSize.Contains(a.Size_Id))) &&
                        (idColor == null || !idColor.Any() || p.Product_Attributes.Any(a => idColor.Contains(a.Color_Id))) &&
                        (idCategory == null || !idCategory.Any() || p.Post_categories.Any(a => idCategory.Contains(a.Category_Id))) &&
                        (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)) &&
                        p.Status == "publish" && p.Type == "product")
                        .Include(pc => pc.Post_categories) // Bao gồm bảng Post_categories từ Posts
                        .ThenInclude(pc => pc.Categories) // Bao gồm Categories từ Post_categories
                    .Include(pt => pt.Post_tags) // Bao gồm bảng Post_tags từ Posts
                        .ThenInclude(pt => pt.Tag) // Bao gồm Tags từ Post_tags
                        .Include(pd => pd.Designer) // Bao gồm Designer từ Posts
                      .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Size) // Bao gồm Size từ Product_Attributes
                     .Include(p => p.Product_Attributes).ThenInclude(pl => pl.Color) // Bao gồm Color từ Product_Attributes
                     .Include(p => p.Product_Attributes).ThenInclude(pm => pm.Material) // Bao gồm Material
                     .Include(p => p.Product_Attributes).ThenInclude(pt => pt.Textile_Technology) // Bao gồm Textile_Technology
                     .Include(p => p.Product_Attributes).ThenInclude(ps => ps.Style) // Bao gồm Style
                     .OrderByDescending(p => p.Created_at) // Sắp xếp giảm dần theo Created_at
                 .Select(p => new Product_Posts
                 {
                     Id = p.Id,
                     STT = p.STT,
                     Title = p.Title,
                     Slug = p.Slug,
                     Status = p.Status,
                     AuthorId = p.Designer.id_Designer,
                     Type = p.Type,
                     Short_description = p.Short_description,
                     Description = p.Description,
                     Image_library = p.Image_library,
                     Feature_image = p.Feature_image,
                     Post_date = p.Post_date,
                     Created_at = p.Created_at,
                     Updated_at = p.Updated_at,
                     Deleted_at = p.Deleted_at,
                     Product_Attributes = p.Product_Attributes, // Bao gồm thuộc tính
                     MinPrice = p.Product_Attributes.Min(pa => pa.Sale_price ?? pa.Regular_price), // Giá thấp nhất
                     MaxPrice = p.Product_Attributes.Max(pa => pa.Sale_price ?? pa.Regular_price)  // Giá cao nhất
                 })
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();


        }

        public async Task<int> GetTotalCountAsyncFilter(List<long?> idDesigner, List<long?> idColor, List<long?> idMaterial, List<long?> idTextile_technology, List<long?> idStyle, List<long?> idSize, List<long?> idCategory, string searchTerm)
        {
            return await _context.Posts
              .CountAsync(p => p.Type == "Product" &&
                        (idDesigner == null || !idDesigner.Any() || idDesigner.Contains(p.AuthorId)) && // Lọc theo danh sách tác giả
                        (idTextile_technology == null || !idTextile_technology.Any() || p.Product_Attributes.Any(a => idTextile_technology.Contains(a.Textile_technology_id))) &&
                        (idStyle == null || !idStyle.Any() || p.Product_Attributes.Any(a => idStyle.Contains(a.Style_id))) &&
                        (idMaterial == null || !idMaterial.Any() || p.Product_Attributes.Any(a => idMaterial.Contains(a.Material_id))) &&
                        (idSize == null || !idSize.Any() || p.Product_Attributes.Any(a => idSize.Contains(a.Size_Id))) &&
                        (idColor == null || !idColor.Any() || p.Product_Attributes.Any(a => idColor.Contains(a.Color_Id))) &&
                        (idCategory == null || !idCategory.Any() || p.Post_categories.Any(a => idCategory.Contains(a.Category_Id))) &&
                        p.Status == "publish" &&
                        p.Type == "product" && 
                        (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task<List<Product_Posts>> GetByTypeAsyncFilter2(string type, List<long?> idDesigner, List<long?> idCategory, int pageNumber, int pageSize, string searchTerm)
        {
            return await _context.Posts
                .Where(p =>
            p.Type == type && p.Status == "publish" &&
            (idDesigner == null || !idDesigner.Any() || idDesigner.Contains(p.AuthorId)) &&
            (idCategory == null || !idCategory.Any() || p.Post_categories.Any(pc => idCategory.Contains(pc.Category_Id))) &&
            (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm))).Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
                .Include(p => p.Designer)
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> GetTotalCountAsyncFilter2(string type, List<long?> idDesigner, List<long?> idCategory, string searchTerm)
        {
            return await _context.Posts
            .CountAsync(p => p.Type == type && p.Status == "publish" &&
            (idDesigner == null || !idDesigner.Any() || idDesigner.Contains(p.AuthorId)) &&
            (idCategory == null || !idCategory.Any() || p.Post_categories.Any(pc => idCategory.Contains(pc.Category_Id))) &&
                     (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }

        public async Task<Product_Posts> GetBySlugAndType(string slug, string type)
        {
            return await _context.Posts
               .Where(p => p.Slug == slug && p.Type == type && p.Status != "delete")
               .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
               .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories)
               .OrderByDescending(p => p.Id)
               .FirstOrDefaultAsync();
        }
        public async Task<bool> CheckSlugForUpdate(string slug, long postId)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            // Tìm kiếm Slug trùng lặp nhưng bỏ qua chính bản ghi hiện tại
            return await _context.Posts
                .AnyAsync(p => p.Slug == slug && p.Status != "delete" && p.Id != postId);
        }

    }
}
