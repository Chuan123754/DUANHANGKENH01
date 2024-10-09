//using appAPI.Repository;
//using appAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace appAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PostsController : ControllerBase
//    {
//        private readonly IRepository<Product_Posts> _postRepository;
//        private readonly IRepository<Product_variants> _postProductRepository;
//        private readonly IRepository<Post_tags> _postTagRepository;
//        private readonly IRepository<Post_categories> _postcategorieRepository;
//        private readonly IRepository<Categories> _categoryRepository;
//        private readonly IRepository<Tags> _tagRepository;

//        public PostsController(
//            IRepository<Product_Posts> postRepository,
//            IRepository<Product_variants> postProductRepository,
//            IRepository<Post_tags> postTagRepository,
//            IRepository<Post_categories> postcategorieRepository,
//            IRepository<Categories> categoryRepository,
//            IRepository<Tags> tagRepository)
//        {
//            _postRepository = postRepository;
//            _postMetaRepository = postMetaRepository;
//            _postProductRepository = postProductRepository;
//            _postTagRepository = postTagRepository;
//            _postcategorieRepository = postcategorieRepository;
//            _categoryRepository = categoryRepository;
//            _tagRepository = tagRepository;
//        }

//        [HttpGet("posts-get")]
//        public IActionResult Get()
//        {
//            return Ok(_postRepository.GetAll());
//        }

//        [HttpGet("posts-get-id")]
//        public IActionResult Get(long id)
//        {
//            var post = _postRepository.GetById(id);
//            if (post == null)
//            {
//                return NotFound("Post not found");
//            }
//            return Ok(post);
//        }

//        [HttpPost("posts-post")]
//        public IActionResult Post(Product_Posts post)
//        {
//            try
//            {
//                if (post.Post_metas != null && post.Post_metas.Any())
//                {
//                    _postMetaRepository.AddRange(post.Post_metas);
//                }

//                if (post.Product_Variants != null && post.Product_Variants.Any())
//                {
//                    _postProductRepository.AddRange(post.Product_Variants);
//                }

//                if (post.Post_tags != null && post.Post_tags.Any())
//                {
//                    _postTagRepository.AddRange(post.Post_tags);
//                }
//                _postRepository.Add(post);
//                return Ok(post);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPut("posts-put")]
//        public IActionResult Put(Product_Posts post)
//        {
//            var item = _postRepository.GetById(post.Id);
//            if (item == null)
//            {
//                return NotFound("Post not found");
//            }

//            item.Title = post.Title;
//            item.Slug = post.Slug;
//            item.Status = post.Status;
//            item.AuthorId = post.AuthorId;
//            item.Type = post.Type;
//            item.Post_date = post.Post_date;
//            item.Created_by = post.Created_by;
//            item.Updated_by = post.Updated_by;
//            item.Deleted_at = post.Deleted_at;
//            item.Created_at = post.Created_at;
//            item.Updated_at = post.Updated_at;

//            _postRepository.Update(item);
//            return Ok(new { message = "Cập nhật bài viết thành công" });
//        }

//        [HttpDelete("posts-delete")]
//        public IActionResult Delete(long id)
//        {
//            var delete = _postRepository.GetById(id);
//            if (delete == null)
//            {
//                return NotFound("Post not found");
//            }

//            _postRepository.Remove(delete);
//            return Ok(new { message = "Xóa bài viết thành công" });
//        }

//        [HttpGet("get-tags-by-post-id")]
//        public IActionResult GetTagsByPostId(long postId)
//        {
//            // Bước 1: Lấy danh sách Tag_Id từ bảng post_tags theo Post_Id
//            var tagIds = _postTagRepository.GetAll()
//                            .Where(pt => pt.Post_Id == postId)
//                            .Select(pt => pt.Tag_Id)
//                            .ToList();

//            // Bước 2: Dùng danh sách Tag_Id để tìm các đối tượng Tag từ bảng tags
//            var tags = _tagRepository.GetAll()
//                        .Where(t => tagIds.Contains(t.Id))
//                        .ToList();

//            if (tags == null || !tags.Any())
//            {
//                return NotFound("No tags found for the specified post.");
//            }

//            return Ok(tags);
//        }


//        [HttpGet("get-categories-by-post-id")]
//        public IActionResult GetCategoriesByPostId(long postId)
//        {
//            // Bước 1: Lấy danh sách Category_Id từ bảng post_categories theo Post_Id
//            var categoryIds = _postcategorieRepository.GetAll()
//                                .Where(pc => pc.Post_Id == postId)
//                                .Select(pc => pc.Category_Id)
//                                .ToList();

//            // Bước 2: Dùng danh sách Category_Id để tìm các đối tượng Category từ bảng categories
//            var categories = _categoryRepository.GetAll()
//                                .Where(c => categoryIds.Contains(c.Id))
//                                .ToList();

//            if (categories == null || !categories.Any())
//            {
//                return NotFound("No categories found for the specified post.");
//            }

//            return Ok(categories);
//        }

//        // Thêm phương thức SearchPosts
//        [HttpGet("search")]
//        public IActionResult SearchPosts(string keyword)
//        {
//            if (string.IsNullOrWhiteSpace(keyword))
//            {
//                return BadRequest("Keyword is required for searching.");
//            }

//            var posts = _postRepository.GetAll()
//                        .Where(p => p.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
//                                    p.Slug.Contains(keyword, StringComparison.OrdinalIgnoreCase))
//                        .ToList();

//            if (!posts.Any())
//            {
//                return NotFound("No posts found matching the given keyword.");
//            }

//            return Ok(posts);
//        }
//    }
//}
