using appAPI.IRepository;
using appAPI.Models;
using appAPI.Models.DTO;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantsDiscountController : ControllerBase
    {
        private readonly IProductAttributeDiscountRepository _repo;

        public VariantsDiscountController(IProductAttributeDiscountRepository productAttributeDiscountRepository)
            
        {
            _repo = productAttributeDiscountRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _repo.GetByIdAndType(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetByProductId")]
        public async Task<IActionResult> Get(long productId)
        {
            var result = await _repo.GetByIdProduct(productId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetByDiscountId")]
        public async Task<IActionResult> GetProductvariants(long discountId)
        {
            var result = await _repo.GetByIdDiscount(discountId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post(P_attribute_discount attributeDiscount)
        {
            try
            {
                await _repo.Create(attributeDiscount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
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
    }

}
