using AppAPI.Repositories;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostMetasController : ControllerBase
    {
        private readonly IRepository<Post_metas> _postMetaRepository;

        public PostMetasController(IRepository<Post_metas> postMetaRepository)
        {
            _postMetaRepository = postMetaRepository;
        }

        [HttpGet("postmetas-get")]
        public IActionResult Get()
        {
            return Ok(_postMetaRepository.GetAll());
        }

        [HttpGet("postmetas-get-id")]
        public IActionResult Get(long id)
        {
            var postMeta = _postMetaRepository.GetById(id);
            if (postMeta == null)
            {
                return NotFound("Post meta not found");
            }
            return Ok(postMeta);
        }

        [HttpPost("postmetas-post")]
        public IActionResult Post(Post_metas postMeta)
        {
            try
            {
                _postMetaRepository.Add(postMeta);
                return Ok(new { message = "Thêm meta cho bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("postmetas-put")]
        public IActionResult Put(Post_metas postMeta)
        {
            var item = _postMetaRepository.GetById(postMeta.Id);
            if (item == null)
            {
                return NotFound("Post meta not found");
            }

            item.Meta_key = postMeta.Meta_key;
            item.Meta_value = postMeta.Meta_value;
            item.Post_Id = postMeta.Post_Id;

            _postMetaRepository.Update(item);
            return Ok(new { message = "Cập nhật meta cho bài viết thành công" });
        }

        [HttpDelete("postmetas-delete")]
        public IActionResult Delete(long id)
        {
            var delete = _postMetaRepository.GetById(id);
            if (delete == null)
            {
                return NotFound("Post meta not found");
            }

            _postMetaRepository.Remove(delete);
            return Ok(new { message = "Xóa meta khỏi bài viết thành công" });
        }
    }
}
