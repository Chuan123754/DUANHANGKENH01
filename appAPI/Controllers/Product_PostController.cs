using appAPI.IRepository;
using appAPI.Models;
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
        private readonly ITagsRepository _tagsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public Product_PostController(IPostReponsetory postRepository, ITagsRepository tagsRepository, ICategoriesRepository categoriesRepository)
        {
            _postRepository = postRepository;
            _tagsRepository = tagsRepository;
            _categoriesRepository = categoriesRepository;
        }
        [HttpGet("Get-All")]
        public async Task<List<Product_Posts>> GetAll()
        {
            return await _postRepository.GetAll();
        }
        [HttpGet("Get-all-type")]
        public async Task<IActionResult> GetAllType(string type)
        {
            // Các hàm để lấy tên danh mục và tên thẻ
            string GetCategoryName(List<Post_categories> lst)
            {
                if (lst == null || lst.Count == 0)
                    return string.Empty;

                return string.Join(",", lst.Select(item => item?.Categories?.Title).Where(title => !string.IsNullOrEmpty(title)));
            }

            string GetTagName(List<Post_tags> lst)
            {
                if (lst == null || lst.Count == 0)
                    return string.Empty;

                return string.Join(",", lst.Select(item => item?.Tag?.Title).Where(title => !string.IsNullOrEmpty(title)));
            }

            // Gọi hàm từ repository để lấy danh sách theo loại
            var list = await _postRepository.GetAllByType(type);

            // Duyệt qua danh sách và tạo đối tượng với các thông tin cần thiết
            var result = list.Select(p => new
            {
                p.Id,
                p.Title,
                p.AuthorId,
                CategoryName = GetCategoryName(p.Post_categories.ToList()),
                TagName = GetTagName(p.Post_tags.ToList()),
                p.Status,
                p.Created_at,
                p.Deleted,
                p.Deleted_at
            }).ToList();

            // Trả về danh sách đã xử lý
            return Ok(result);
        }

        // Lấy sản phẩm theo ID
        [HttpGet("BetGyIdType")]
        public async Task<IActionResult> GetByIdProduct(long id, string type)
        {
            var item = await _postRepository.GetByIdAndType(id, type);
            if (item == null)
            {
                return NotFound("Not found");
            }
            return Ok(item);
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
        [HttpPut("Edit-post")]
        public async Task EditPost(Product_Posts posts)
        {
            await _postRepository.Update(posts);
        }

        [HttpDelete("Delete-post")]
        public async Task<IActionResult> Delete(long id)
        {
            await _postRepository.Delete(id);
            return Ok(new { message = "Xoá thành công" });
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            string GetCategoryName(List<Post_categories> lst)
            {
                if (lst == null || lst.Count == 0)
                    return string.Empty;

                return string.Join(",", lst.Select(item => item?.Categories?.Title).Where(title => !string.IsNullOrEmpty(title)));
            }


            string GetTagName(List<Post_tags> lst)
            {
                if (lst == null || lst.Count == 0)
                    return string.Empty;

                return string.Join(",", lst.Select(item => item?.Tag?.Title).Where(title => !string.IsNullOrEmpty(title)));
            };

            var list = await _postRepository.GetByTypeAsync(type, pageNumber, pageSize, searchTerm);

            var result = list.Select(p => new
            {
                p.Id,
                p.Title,
                p.AuthorId,
                CategoryName = GetCategoryName(p.Post_categories.ToList()),
                TagName = GetTagName(p.Post_tags.ToList()),
                p.Status,
                p.Created_at,
                p.Deleted,
                p.Deleted_at
            }).ToList();
            return Ok(result);
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
    }
}
