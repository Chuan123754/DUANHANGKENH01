using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttribute_wishlist_Controller : ControllerBase
    {
        private readonly IProduct_wishlist_Reponsitory _reponsitory;
        public ProductAttribute_wishlist_Controller(IProduct_wishlist_Reponsitory reponsitory)
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
        public async Task Create(Product_wishlist pwl)
        {
            await _reponsitory.Create(pwl);
        }
        [HttpDelete("DeletePWL")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _reponsitory.Delete(id); // Đảm bảo hàm Delete là async
            if (result == "Xoá sản phẩm trong wishlist thành công")
            {
                return Ok(result); // Trả về thông báo thành công
            }
            return BadRequest(result); // Trả về thông báo lỗi
        }

    }
}
