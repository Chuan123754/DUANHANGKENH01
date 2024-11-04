using appAPI.IRepository;
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

    }
}
