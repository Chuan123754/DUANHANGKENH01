using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Variants_WishlistController : ControllerBase
    {
        private readonly IRepository<Product_variants_wishlist> _repon;

        public Variants_WishlistController(IRepository<Product_variants_wishlist> productwishlist)
        {
            _repon = productwishlist;
        }

        [HttpGet("variantwishlist-get")]
        public IActionResult Get()
        {
            return Ok(_repon.GetAll());
        }

        [HttpGet("variantwishlist-get-id")]
        public IActionResult Get(long id)
        {
            var productlist = _repon.GetById(id);
            if (productlist == null)
            {
                return NotFound("Product wishlist not found");
            }
            return Ok(productlist);
        }

        [HttpPost("variantwishlist-post")]
        public IActionResult Post([FromBody] Product_variants_wishlist productlist)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = errors });
            }

            try
            {

                _repon.Add(productlist);
                return Ok(new { message = "Thêm thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi thêm category", error = ex.Message });
            }
        }

        [HttpPut("variantwishlist-put")]
        public IActionResult Put(Product_variants_wishlist productlist)
        {
            var item = _repon.GetById(productlist.Id);
            if (item == null)
            {
                return NotFound("Itiem not found");
            }

            item.Wishlist_id = productlist.Wishlist_id;
            item.Product_variants_id = productlist.Product_variants_id;

            _repon.Update(item);
            return Ok(new { message = "Cập nhật  thành công" });
        }

        [HttpDelete("variantwishlist-delete")]
        public IActionResult Delete(long id)
        {
            var delete = _repon.GetById(id);
            if (delete == null)
            {
                return NotFound("Itiem not found");
            }

            _repon.Remove(delete);
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
