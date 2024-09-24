using AppAPI.Repositories;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagsController : ControllerBase
    {
        private readonly IRepository<Post_tags> _postTagRepository;

        public PostTagsController(IRepository<Post_tags> postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }

        [HttpGet("posttags-get")]
        public IActionResult Get()
        {
            return Ok(_postTagRepository.GetAll());
        }

        [HttpGet("posttags-get-id")]
        public IActionResult Get(long id)
        {
            var postTag = _postTagRepository.GetById(id);
            if (postTag == null)
            {
                return NotFound("Post tag not found");
            }
            return Ok(postTag);
        }

        [HttpPost("posttags-post")]
        public IActionResult Post([FromBody] Post_tags postTag)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = errors });
            }

            try
            {
                _postTagRepository.Add(postTag);
                return Ok(new { message = "Thêm tag cho bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi thêm tag", error = ex.Message });
            }
        }

        [HttpPut("posttags-put")]
        public IActionResult Put(Post_tags postTag)
        {
            var item = _postTagRepository.GetById(postTag.Id);
            if (item == null)
            {
                return NotFound("Post tag not found");
            }

            item.Post_Id = postTag.Post_Id;
            item.Tag_Id = postTag.Tag_Id;

            _postTagRepository.Update(item);
            return Ok(new { message = "Cập nhật tag cho bài viết thành công" });
        }

        [HttpDelete("posttags-delete")]
        public IActionResult Delete(long id)
        {
            var delete = _postTagRepository.GetById(id);
            if (delete == null)
            {
                return NotFound("Post tag not found");
            }

            _postTagRepository.Remove(delete);
            return Ok(new { message = "Xóa tag khỏi bài viết thành công" });
        }
    }
}
