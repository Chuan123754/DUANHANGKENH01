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
        [HttpGet("GetAllProductAttributesDelete")]
        public async Task<IActionResult> GetAllProductAttributesDelete()
        {
            var result = await _repo.GetAllProductAttributesDelete();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetProductAttributesByPostId")]
        public async Task<IActionResult> GetProductAttributesByProductVariantId(long id)
        {
            var result = await _repo.GetProductAttributesByPostId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetProductAttributesByPostIdClient")]
        public async Task<IActionResult> GetProductAttributesByProductVarianIdClient(long id)
        {
            var result = await _repo.GetProductAttributesByPostIdClient(id);
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
        [HttpDelete("DeleteProductAttributeCung")]
        public async Task<IActionResult> DeleteCung(long id)
        {
            try
            {
                await _repo.DeleteCung(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Restore-productattribute")]
        public async Task<IActionResult> Restore(long id)
        {
            try
            {
                await _repo.Restore(id);
                return Ok(new { message = "Khôi phục thành công" });
            }
            catch (Exception ex)
            {
                // Trả về lỗi 400 kèm thông báo lỗi
                return BadRequest(new { message = ex.Message });
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
