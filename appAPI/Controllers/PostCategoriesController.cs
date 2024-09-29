using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoriesController : ControllerBase
    {
        private readonly IRepository<Post_categories> _postCategoryRepository;

        public PostCategoriesController(IRepository<Post_categories> postCategoryRepository)
        {
            _postCategoryRepository = postCategoryRepository;
        }

        [HttpGet("postcategories-get")]
        public IActionResult Get()
        {
            return Ok(_postCategoryRepository.GetAll());
        }

        [HttpGet("postcategories-get-id")]
        public IActionResult Get(long id)
        {
            var postCategory = _postCategoryRepository.GetById(id);
            if (postCategory == null)
            {
                return NotFound("Post category not found");
            }
            return Ok(postCategory);
        }

        [HttpPost("postcategories-post")]
        public IActionResult Post([FromBody] Post_categories postCategory)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = errors });
            }

            try
            {
                
                _postCategoryRepository.Add(postCategory);
                return Ok(new { message = "Thêm category cho bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi thêm category", error = ex.Message });
            }
        }

        [HttpPut("postcategories-put")]
        public IActionResult Put(Post_categories postCategory)
        {
            var item = _postCategoryRepository.GetById(postCategory.Id);
            if (item == null)
            {
                return NotFound("Post category not found");
            }

            item.Post_Id = postCategory.Post_Id;
            item.Category_Id = postCategory.Category_Id;

            _postCategoryRepository.Update(item);
            return Ok(new { message = "Cập nhật category cho bài viết thành công" });
        }

        [HttpDelete("postcategories-delete")]
        public IActionResult Delete(long id)
        {
            var delete = _postCategoryRepository.GetById(id);
            if (delete == null)
            {
                return NotFound("Post category not found");
            }

            _postCategoryRepository.Remove(delete);
            return Ok(new { message = "Xóa category khỏi bài viết thành công" });
        }
    }
}
