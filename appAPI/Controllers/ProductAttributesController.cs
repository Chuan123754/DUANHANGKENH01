using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributesController : ControllerBase
    {
        private readonly IProductAttributesRepository _repo;

        public ProductAttributesController(IProductAttributesRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllProductAttributes")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAllProductAttributes();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetProductAttributesByProductVariantId")]
        public async Task<IActionResult> GetProductAttributesByProductVariantId(long id)
        {
            var result = await _repo.GetProductAttributesByProductVarianId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetProductAttributesByProductVarianIdClient")]
        public async Task<IActionResult> GetProductAttributesByProductVarianIdClient(long id)
        {
            var result = await _repo.GetProductAttributesByProductVarianIdClient(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetProductAttributeById")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _repo.GetProductAttributesById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost("CreateProductAttrubute")]
        public async Task<IActionResult> Create(Product_Attributes producesAttribute)
        {
            try
            {
                await _repo.Create(producesAttribute);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateProductAttrubutes")]
        public async Task<IActionResult> Update(Product_Attributes producesAttribute, long id)
        {
            try
            {
                await _repo.Update(producesAttribute, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteProductAttribute")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _repo.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repo.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }

    }
}
