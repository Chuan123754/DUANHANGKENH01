using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class PostReponsetory : IPostReponsetory
    {
        private readonly APP_DATA_DATN _context;
        public PostReponsetory()
        {
            _context = new APP_DATA_DATN();
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
        public async Task<List<Product_Posts>> GetAll()
        {
            return await _context.Posts
              .Include(p => p.Post_tags).ThenInclude(pt => pt.Tag)
              .Include(p => p.Post_categories).ThenInclude(p => p.Categories)
              .ToListAsync();
        }

        public async Task<Product_Posts> GetByIdAndType(long id, string type)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id && p.Type == type && p.Deleted == false);
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

        public async Task Update(Product_Posts post)
        {
            var item = _context.Posts.Find(post.Id);
            if (item == null)
            {
                return;
            }
            item.Title = post.Title;
            item.Slug = post.Slug;
            item.Status = post.Status;
            item.AuthorId = post.AuthorId;
            item.Updated_by = post.Updated_by;
            item.Updated_at = DateTime.Now;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
