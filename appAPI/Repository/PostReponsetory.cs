using appAPI.IRepository;
using appAPI.Models;
using appAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class PostReponsetory : IPostReponsetory
    {
        private readonly APP_DATA_DATN _context;
        public PostReponsetory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task<Product_Posts> CreatePost(Product_Posts post, List<long> tagIds, List<long> category)
        {
            post.Type = "post";
            post.Deleted = false;
            post.Created_at = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            foreach (var tag in tagIds)
            {
                var postTag = new Post_tags
                {
                    Post_Id = post.Id,
                    Tag_Id = tag
                };
                _context.Post_Tags.Add(postTag);
                await _context.SaveChangesAsync();
            }
            foreach (var cate in category)
            {
                var postCate = new Post_categories
                {
                    Post_Id = post.Id,
                    Category_Id = cate
                };
                _context.Post_Categories.Add(postCate);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Product_Posts> CreatePage(Product_Posts post, List<long> tagIds)
        {
            post.Type = "page";
            post.Deleted = false;
            post.Created_at = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            foreach (var tag in tagIds)
            {
                var postTag = new Post_tags
                {
                    Post_Id = post.Id,
                    Tag_Id = tag
                };
                _context.Post_Tags.Add(postTag);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Product_Posts> CreateProduct(Product_Posts post, List<long> tagIds, List<long> category)
        {
            post.Type = "product";
            post.Deleted = false;
            post.Created_at = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            foreach (var tag in tagIds)
            {
                var postTag = new Post_tags
                {
                    Post_Id = post.Id,
                    Tag_Id = tag
                };
                _context.Post_Tags.Add(postTag);
                await _context.SaveChangesAsync();
            }
            foreach (var cate in category)
            {
                var postCate = new Post_categories
                {
                    Post_Id = post.Id,
                    Category_Id = cate
                };
                _context.Post_Categories.Add(postCate);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Product_Posts> CreateProject(Product_Posts post, List<long> tagIds, List<long> category)
        {
            post.Type = "project";           
            post.Deleted = false;
            post.Created_at = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            foreach (var tag in tagIds)
            {
                var postTag = new Post_tags
                {
                    Post_Id = post.Id,
                    Tag_Id = tag
                };
                _context.Post_Tags.Add(postTag);
                await _context.SaveChangesAsync();
            }
            foreach (var cate in category)
            {
                var postCate = new Post_categories
                {
                    Post_Id = post.Id,
                    Category_Id = cate
                };
                _context.Post_Categories.Add(postCate);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return post;
        }
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

        public async Task<List<Product_Posts>> GetAllByType(string type)
        {
            return await _context.Posts.Where(p => p.Type == type && p.Deleted == false)
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Post_categories).ThenInclude(p => p.Categories)
                .ToListAsync();
        }
        public async Task<List<ModelPostTag>> GetAll()
        {
            // Lấy tất cả sản phẩm với các thông tin liên quan
            var objPosts = await _context.Post_Tags
                .Include(s => s.Posts)
                .Select(s => new ProductModel
                {
                    id = s.Post_Id,
                    Title = s.Posts.Title,
                    slug = s.Posts.Slug,
                    type = s.Posts.Type,
                    status = s.Posts.Status,
                    AuthorId = s.Posts.AuthorId,
                    Deleted = s.Posts.Deleted,
                    product_video = s.Posts.product_video,
                    Short_description = s.Posts.Short_description,
                    Description = s.Posts.Description,
                    Feature_image = s.Posts.Feature_image,
                    Image_library = s.Posts.Image_library,
                })
                .ToListAsync();

            // Lấy tất cả thẻ và danh mục liên quan đến từng sản phẩm
            var postTags = new List<ModelPostTag>();

            foreach (var post in objPosts)
            {
                // Lấy các thẻ liên quan đến sản phẩm hiện tại
                var lstTags = await _context.Post_Tags
                    .Where(s => s.Post_Id == post.id)
                    .Select(s => new TagDTO
                    {
                        Id = s.Tag.Id,
                        Name = s.Tag.Title
                    })
                    .ToListAsync();

                // Lấy các danh mục liên quan đến sản phẩm hiện tại
                var lstCategories = await _context.Post_Categories
                    .Where(s => s.Post_Id == post.id)
                    .Select(s => new CategoryDTO
                    {
                        Id = s.Categories.Id,
                        Name = s.Categories.Title
                    })
                    .ToListAsync();

                // Tạo ModelPostTag cho sản phẩm và thêm vào danh sách
                var postTag = new ModelPostTag
                {
                    objPost = post,
                    lstTags = lstTags,
                    lstCategories = lstCategories
                };

                postTags.Add(postTag);
            }

            return postTags;
        }

        public async Task<ModelPostTag> GetByIdAndType(long id, string type)
        {
            var objpost = _context.Post_Tags.Include(s=>s.Posts).Where(s => s.Post_Id == id).Select(s=>new ProductModel
            {
                id = s.Post_Id,
         
                Title = s.Posts.Title,
                slug = s.Posts.Slug,
                type = type,
                status = s.Posts.Status,
                AuthorId = s.Posts.AuthorId,            
                Deleted = s.Posts.Deleted,
                product_video = s.Posts.product_video,
                Short_description = s.Posts.Short_description,
                Description = s.Posts.Description,
                Feature_image = s.Posts.Feature_image,
                Image_library = s.Posts.Image_library,
        

            }).FirstOrDefault();

            var lstTags = _context.Post_Tags.Include(s => s.Posts).Where(s => s.Post_Id == id).Select(s=> new TagDTO
            {
                Id = s.Tag.Id,
                Name = s.Tag.Title
            }).ToList();
            var lstCate = _context.Post_Categories.Include(s => s.Posts).Where(s => s.Post_Id == id).Select(s => new CategoryDTO
            {
                Id = s.Categories.Id,
                Name = s.Categories.Title
            }).ToList();


            ModelPostTag post_Tag = new ModelPostTag();
            post_Tag.objPost = objpost;
            post_Tag.lstTags = lstTags;
            post_Tag.lstCategories = lstCate;

            return post_Tag;
        }

        public async Task<List<Product_Posts>> GetByTypeAsync(string type, int pageNumber, int pageSize, string? searchTerm)
        {
            return await _context.Posts
                .Where(p => p.Type == type && (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm) && p.Deleted == false))
                .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag) // Include thông tin từ bảng Tag
                .Include(p => p.Post_categories).ThenInclude(pc => pc.Categories) // Include thông tin từ bảng Categories
                .OrderBy(p => p.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync(string type, string? searchTerm)
        {
            // Lấy tổng số sản phẩm theo loại và tìm kiếm với điều kiện Deleted = false
            return await _context.Posts
                .CountAsync(p => p.Type == type && p.Deleted == false &&
                                (string.IsNullOrEmpty(searchTerm) || p.Title.Contains(searchTerm)));
        }
    

        public async Task Update(Product_Posts post, List<long> tagIds)
        {
            // Kiểm tra xem post có tồn tại trong cơ sở dữ liệu không
            var item = await _context.Posts.FindAsync(post.Id);
            if (item == null)
            {
                return; // Nếu không tìm thấy post, thoát hàm
            }

            // Cập nhật thông tin của post
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

            _context.Posts.Update(item); // Cập nhật post trong ngữ cảnh

            // Cập nhật danh sách tags
            var existingTagIds = _context.Post_Tags
                                         .Where(pt => pt.Post_Id == post.Id)
                                         .Select(pt => pt.Tag_Id)
                                         .ToList();

            // Tìm các tags cần thêm
            var tagsToAdd = tagIds.Except(existingTagIds).ToList();
            foreach (var tagId in tagsToAdd)
            {
                var postTag = new Post_tags
                {
                    Post_Id = post.Id,
                    Tag_Id = tagId
                };
                _context.Post_Tags.Add(postTag);
            }

            // Tìm các tags cần xóa
            var tagsToRemove = existingTagIds.Except(tagIds).ToList();
            var postTagsToRemove = _context.Post_Tags
                                            .Where(pt => pt.Post_Id == post.Id && tagsToRemove.Contains(pt.Tag_Id))
                                            .ToList();
            _context.Post_Tags.RemoveRange(postTagsToRemove);

            // Lưu tất cả thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }



        public async Task Updatetagcate(Product_Posts post, List<long> tagIds, List<long> categoryIds)
        {
            // Kiểm tra xem post có tồn tại trong cơ sở dữ liệu không
            var item = await _context.Posts.FindAsync(post.Id);
            if (item == null)
            {
                return; // Nếu không tìm thấy post, thoát hàm
            }

            // Cập nhật thông tin của post
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

            _context.Posts.Update(item); // Cập nhật post trong ngữ cảnh

            // Cập nhật danh sách tags
            var existingTagIds = _context.Post_Tags
                                         .Where(pt => pt.Post_Id == post.Id)
                                         .Select(pt => pt.Tag_Id)
                                         .ToList();

            // Tìm các tags cần thêm
            var tagsToAdd = tagIds.Except(existingTagIds).ToList();
            foreach (var tagId in tagsToAdd)
            {
                var postTag = new Post_tags
                {
                    Post_Id = post.Id,
                    Tag_Id = tagId
                };
                _context.Post_Tags.Add(postTag);
            }

            // Tìm các tags cần xóa
            var tagsToRemove = existingTagIds.Except(tagIds).ToList();
            var postTagsToRemove = _context.Post_Tags
                                            .Where(pt => pt.Post_Id == post.Id && tagsToRemove.Contains(pt.Tag_Id))
                                            .ToList();
            _context.Post_Tags.RemoveRange(postTagsToRemove);
            var existingCate = _context.Post_Categories
                                       .Where(ct => ct.Post_Id == post.Id)
                                       .Select (ct => ct.Category_Id)
                                       .ToList();
            var cateAdd = categoryIds .Except(existingCate).ToList();
            foreach(var cateId in cateAdd)
            {
                var postCate = new Post_categories
                {
                    Post_Id = post.Id,
                    Category_Id = cateId
                };
                _context.Post_Categories.Add(postCate);
            }
            // Lưu tất cả thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }
    }
}
