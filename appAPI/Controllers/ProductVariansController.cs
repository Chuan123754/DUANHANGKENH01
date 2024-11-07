using appAPI.DTO;
using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariansController : ControllerBase
    {
        private readonly IProductVariantsRepository _repo;

        public ProductVariansController(IProductVariantsRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAllProductVarians();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _repo.GetProductVariantsById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(Product_variants productVariant)
        {
            try
            {
                await _repo.Create(productVariant);
                return Ok(productVariant.Id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update(Product_variants productVariant,long id)
        {
            try
            {            
                await _repo.Update(productVariant,id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _repo.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string type, [FromQuery] string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required.");
            }

            var totalCount = await _repo.GetTotalCountAsync(type, searchTerm);
            return Ok(totalCount);
        }
    }
}
