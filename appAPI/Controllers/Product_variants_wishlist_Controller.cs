using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_variants_wishlist_Controller : ControllerBase
    {
        private readonly IProduct_variants_wishlist_Reponsitory _reponsitory;
        public Product_variants_wishlist_Controller(IProduct_variants_wishlist_Reponsitory reponsitory)
        {
            _reponsitory = reponsitory;
        }
        [HttpGet("GetAllWLP")]
        public async Task<IActionResult> GetAllWLP()
        {
            var result = await _reponsitory.GetAllPW();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetWLPById")]
        public async Task<IActionResult> GetWLPById(long id)
        {
            var result = await _reponsitory.GetByID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost("CreateWLP")]
        public async Task Create(Product_variants_wishlist pwl)
        {
            await _reponsitory.Create(pwl);
        }
        [HttpDelete("DeletePWL")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = _reponsitory.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
