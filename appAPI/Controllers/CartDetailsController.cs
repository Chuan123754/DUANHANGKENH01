using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private readonly APP_DATA_DATN _context;

        public CartDetailsController(APP_DATA_DATN context)
        {
            _context = context;
        }


        // GET: api/cartdetails-get
        [HttpGet("cartdetails-get")]
        public IActionResult Get()
        {
            var cartDetails = _context.Cart_Details
                .Include(cd => cd.Carts)
                .Include(cd => cd.Product_Attributes)
                .Include(cd => cd.Product_Attributes).ThenInclude(cd=>cd.Color)
                .Include(cd => cd.Product_Attributes).ThenInclude(cd=>cd.Size)
                .Include(cd => cd.Product_Attributes).ThenInclude(cd=>cd.Size)               
                .ToList();
            return Ok(cartDetails);
        }

        // GET: api/cartdetails-get-id/{id}
        [HttpGet("cartdetails-get-id/{id}")]
        public IActionResult Get(long id)
        {
            var cartDetail = _context.Cart_Details
                .Include(cd => cd.Carts)
                .Include(cd => cd.Product_Attributes)
                .FirstOrDefault(cd => cd.Id == id);

            if (cartDetail == null)
            {
                return NotFound("Cart detail not found");
            }

            return Ok(cartDetail);
        }

        // POST: api/cartdetails-post
        [HttpPost("cartdetails-post")]
        public ActionResult Post(Cart_details cartDetail)
        {
            try
            {
                var existingCart = _context.Carts.Find(cartDetail.Cart_id);
                if (existingCart == null)
                {
                    return NotFound(new { message = "Giỏ hàng không tồn tại" });
                }

                cartDetail.Carts = existingCart;

                _context.Cart_Details.Add(cartDetail);
                _context.SaveChanges();

                return Ok(new { message = "Thêm chi tiết giỏ hàng thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/cartdetails-put
        [HttpPut("cartdetails-put")]
        public IActionResult Put(Cart_details cartDetail)
        {
            var item = _context.Cart_Details.Find(cartDetail.Id);
            if (item == null)
            {
                return NotFound("Cart detail not found");
            }

            item.Product_id = cartDetail.Product_id;
            item.Cart_id = cartDetail.Cart_id;
            item.Quantity = cartDetail.Quantity;
            item.Status = cartDetail.Status;

            _context.SaveChanges();
            return Ok(new { message = "Cập nhật chi tiết giỏ hàng thành công" });
        }

        // DELETE: api/cartdetails-delete/{id}
        [HttpDelete("cartdetails-delete/{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _context.Cart_Details.Find(id);
            if (delete == null)
            {
                return NotFound("Cart detail not found");
            }

            _context.Cart_Details.Remove(delete);
            _context.SaveChanges();
            return Ok(new { message = "Xóa chi tiết giỏ hàng thành công" });
        }
    }
}
