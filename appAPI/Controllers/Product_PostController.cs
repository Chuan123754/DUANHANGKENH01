using appAPI.IRepository;
using appAPI.Models;
using appAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_PostController : ControllerBase
    {
        private readonly IPostReponsetory _postRepository;
        private readonly IBannerRepository _bannerRepository;
        public Product_PostController(IPostReponsetory postRepository, IBannerRepository bannerRepository)
        {
            _postRepository = postRepository;
            _bannerRepository = bannerRepository;
        }
        [HttpGet("Get-All")]
        public async Task<List<Product_Posts>> GetAll()
        {
            return await _postRepository.GetAll();
        }
        [HttpGet("GetAllByClient")]
        public async Task<List<Product_variants>> GetAllByClient()
        {
            return await _postRepository.GetAllByClient();
        }
        [HttpGet("GetAllProductDelete")]
        public async Task<List<Product_Posts>> GetAllProductDelete()
        {
            return await _postRepository.GetAllProductDelete();
        }
        [HttpGet("Get-all-type")]
        public async Task<IActionResult> GetAllType(string type)
        {     
            var list = await _postRepository.GetAllByType(type);
            return Ok(list);
        }
        [HttpGet("Get-all-client-type-cate")]
        public async Task<IActionResult> GetAllByClientTypeCate(string type, string cate)
        {
            var list = await _postRepository.GetAllByClientTypeCate(type, cate);
            return Ok(list);
        }
        [HttpGet("GetCountByType")]
        public async Task<IActionResult> GetCountByType(string type, long designerId)
        {
            var list = await _postRepository.GetCountByType(type, designerId);
            return Ok(list);
        }
        // Lấy sản phẩm theo ID và loại
        [HttpGet("GetByIdAndType")]
        public async Task<IActionResult> GetByIdProduct(long id, string type)
        {
            var post = await _postRepository.GetByIdAndType(id, type);
            return Ok(post);
        }
        [HttpPost("Create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] Product_Posts post, [FromQuery] List<long> tagIds, [FromQuery] List<long> cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _postRepository.CreateProduct(post, tagIds, cate);
            return Ok(new { message = "Thêm sản phẩm thành công", Post_Id = createdProduct.Id });
        }
        [HttpPost("Create-post")]
        public async Task<IActionResult> CreatePost([FromBody] Product_Posts post, [FromQuery] List<long> tagIds, [FromQuery] List<long> cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPost = await _postRepository.CreatePost(post, tagIds, cate);
            return Ok(new { message = "Thêm bài viết thành công", Post_Id = createdPost.Id });
        }
        // Tạo trang
        [HttpPost("Create-page")]
        public async Task<IActionResult> CreatePage([FromBody] Product_Posts post, [FromQuery] List<long> tagIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPage = await _postRepository.CreatePage(post, tagIds);
            return Ok(new { message = "Thêm trang thành công", Post_Id = createdPage.Id });
        }

        // Tạo dự án
        [HttpPost("Create-project")]
        public async Task<IActionResult> CreateProject([FromBody] Product_Posts post, [FromQuery] List<long> tagIds, [FromQuery] List<long> cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdProject = await _postRepository.CreateProject(post, tagIds, cate);
            return Ok(new { message = "Thêm dự án thành công", Post_Id = createdProject.Id });
        }
        // Chỉnh sửa bài viết
        [HttpPut("Edit-post")]
        public async Task<IActionResult> EditPost([FromBody] Product_Posts post, [FromQuery] List<long> tagIds)
        {
            if (post == null || post.Id <= 0)
            {
                return BadRequest("Invalid post data.");
            }

            var updateProducpost = await _postRepository.Update(post, tagIds);
            return Ok(new { message = "Sửa thành công", Post_Id = updateProducpost.Id });
        }

        // Chỉnh sửa bài viết và các tags, categories
        [HttpPut("Edit-posttagscate")]
        public async Task<IActionResult> EditPostTagsCate([FromBody] Product_Posts post, [FromQuery] List<long> tagIds, [FromQuery] List<long> categoryIds)
        {
            if (post == null || post.Id <= 0)
            {
                return BadRequest("Invalid post data.");
            }

           var updateProducpost = await _postRepository.Updatetagcate(post, tagIds, categoryIds);
            return Ok(new { message = "Sửa thành công", Post_Id = updateProducpost.Id });
        }

        // Xoá bài viết
        [HttpDelete("Delete-post")]
        public async Task<IActionResult> Delete(long id)
        {
            await _postRepository.Delete(id);
            return Ok(new { message = "Xoá thành công" });
        }


        // Xoá bài viết
        [HttpDelete("Restore-post")]
        public async Task<IActionResult> Restore(long id)
        {
            await _postRepository.Restore(id);
            return Ok(new { message = "Khôi phục thành công" });
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _postRepository.GetByTypeAsync(type, pageNumber, pageSize, searchTerm);        
            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string type, [FromQuery] string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required.");
            }

            var totalCount = await _postRepository.GetTotalCountAsync(type, searchTerm);
            return Ok(totalCount);
        }
        [HttpGet("get-by-type-product")]
        public async Task<IActionResult> GetByTypeProduct([FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _postRepository.GetByTypeAsyncProduct(type, pageNumber, pageSize, searchTerm);
            return Ok(list);
        }
        [HttpGet("Get-Total-Count-Product")]
        public async Task<IActionResult> GetTotalCountProduct([FromQuery] string type, [FromQuery] string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required.");
            }

            var totalCount = await _postRepository.GetTotalCountAsyncProduct(type, searchTerm);
            return Ok(totalCount);
        }
        [HttpGet("get-by-type-delete")]
        public async Task<IActionResult> GetByTypeDelete([FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _postRepository.GetByTypeAsyncDelete(type, pageNumber, pageSize, searchTerm);
            return Ok(list);
        }
        [HttpGet("Get-Total-Count-Delete")]
        public async Task<IActionResult> GetTotalCountDelete([FromQuery] string type, [FromQuery] string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required.");
            }

            var totalCount = await _postRepository.GetTotalCountAsyncDelete(type, searchTerm);
            return Ok(totalCount);
        }
        [HttpGet("get-by-type-cate")]
        public async Task<IActionResult> GetByTypeCate([FromQuery] string type, [FromQuery] long categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _postRepository.GetByTypeAsyncCate(type, categoryId, pageNumber, pageSize);
            return Ok(list);
        }
        [HttpGet("Get-Total-Count-Cate")]
        public async Task<IActionResult> GetTotalCountCate([FromQuery] string type, [FromQuery] long categoryId)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required.");
            }

            var totalCount = await _postRepository.GetTotalCountAsyncCate(type, categoryId);
            return Ok(totalCount);
        }
        [HttpGet("Get-Name-Designer")]
        public async Task<string> GetNameDesigner(long id)
        {
            return await _postRepository.GetNameDesigner(id);
        }
    }
}
