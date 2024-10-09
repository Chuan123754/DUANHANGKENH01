using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_PostController : ControllerBase
    {
        private readonly IPostReponsetory _postRepository;
        public Product_PostController(IPostReponsetory postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet("Get-all-product")]
        public async Task<List<Product_Posts>> GetAllProduct()
        {
            return await _postRepository.GetAllByType("product");
        }
        [HttpGet("Get-all-page")]
        public async Task<List<Product_Posts>> GetAllPage()
        {
            return await _postRepository.GetAllByType("page");
        }
        [HttpGet("Get-all-post")]
        public async Task<List<Product_Posts>> GetAllPost()
        {
            return await _postRepository.GetAllByType("post");
        }

        // Lấy sản phẩm theo ID
        [HttpGet("BetGyId-product")]
        public async Task<IActionResult> GetByIdProduct(long id)
        {
            var product = await _postRepository.GetByIdAndType(id, "product");
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        // Lấy sản phẩm theo ID
        [HttpGet("BetGyId-page")]
        public async Task<IActionResult> GetByIdPage(long id)
        {
            var product = await _postRepository.GetByIdAndType(id, "page");
            if (product == null)
            {
                return NotFound("Page not found");
            }
            return Ok(product);
        }
        // Lấy sản phẩm theo ID
        [HttpGet("BetGyId-post")]
        public async Task<IActionResult> GetByIdPost(long id)
        {
            var product = await _postRepository.GetByIdAndType(id, "post");
            if (product == null)
            {
                return NotFound("Post not found");
            }
            return Ok(product);
        }

        [HttpPost("Create-product")]
        public async Task CreateProduct(Product_Posts post)
        {
            await _postRepository.CreateProduct(post);
        }
        [HttpPost("Create-post")]
        public async Task CreatePost(Product_Posts post)
        {
            await _postRepository.CreatePost(post);
        }
        [HttpPost("Create-page")]
        public async Task CreatePage(Product_Posts post)
        {
            await _postRepository.CreatePage(post);
        }
        [HttpPut("Edit-post")]
        public async Task EditPost(Product_Posts posts)
        {
            await _postRepository.Update(posts);
        }

        [HttpDelete("Delete-post")]
        public async Task Delete(long id)
        {
            await _postRepository.Delete(id);
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchTerm = "")
        {
            var posts = await _postRepository.GetByTypeAsync(type, pageNumber, pageSize, searchTerm);
            return Ok(posts);
        }
    }
}
