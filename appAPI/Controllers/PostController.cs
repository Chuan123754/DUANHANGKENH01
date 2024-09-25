using AppAPI.Repositories;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Posts> _postRepository;
        private readonly IRepository<Post_metas> _postMetaRepository;
        private readonly IRepository<Products> _postProductRepository;
        private readonly IRepository<Post_tags> _postTagRepository;

        public PostsController(
            IRepository<Posts> postRepository,
            IRepository<Post_metas> postMetaRepository,
            IRepository<Products> postProductRepository,
            IRepository<Post_tags> postTagRepository)
        {
            _postRepository = postRepository;
            _postMetaRepository = postMetaRepository;
            _postProductRepository = postProductRepository;
            _postTagRepository = postTagRepository;
        }

        [HttpGet("posts-get")]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAll());
        }

        [HttpGet("posts-get-id")]
        public IActionResult Get(long id)
        {
            var post = _postRepository.GetById(id);
            if (post == null)
            {
                return NotFound("Post not found");
            }
            return Ok(post);
        }

        [HttpPost("posts-post")]
        public IActionResult Post(Posts post)
        {
            try
            {
                if (post.Post_metas != null && post.Post_metas.Any())
                {
                    _postMetaRepository.AddRange(post.Post_metas);
                }

                if (post.Post_products != null && post.Post_products.Any())
                {
                    _postProductRepository.AddRange(post.Post_products);
                }

                if (post.Post_tags != null && post.Post_tags.Any())
                {
                    _postTagRepository.AddRange(post.Post_tags);
                }

                _postRepository.Add(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("posts-put")]
        public IActionResult Put(Posts post)
        {
            var item = _postRepository.GetById(post.Id);
            if (item == null)
            {
                return NotFound("Post not found");
            }

            item.Title = post.Title;
            item.Slug = post.Slug;
            item.Status = post.Status;
            item.AuthorId = post.AuthorId;
            item.Type = post.Type;
            item.Post_date = post.Post_date;
            item.Created_by = post.Created_by;
            item.Updated_by = post.Updated_by;
            item.Deleted_at = post.Deleted_at;
            item.Created_at = post.Created_at;
            item.Updated_at = DateTime.Now;

            _postRepository.Update(item);
            return Ok(new { message = "Cập nhật bài viết thành công" });
        }

        [HttpDelete("posts-delete")]
        public IActionResult Delete(long id)
        {
            var delete = _postRepository.GetById(id);
            if (delete == null)
            {
                return NotFound("Post not found");
            }

            _postRepository.Remove(delete);
            return Ok(new { message = "Xóa bài viết thành công" });
        }

        [HttpGet("get-tags-by-post-id")]
        public IActionResult GetTagsByPostId(long postId)
        {
            var tags = _postTagRepository.GetAll()
                         .Where(pt => pt.Post_Id == postId)
                         .ToList();

            if (tags == null || !tags.Any())
            {
                return NotFound("No tags found for the specified post.");
            }

            return Ok(tags);
        }

    }
}
