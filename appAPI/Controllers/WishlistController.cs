using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        APP_DATA_DATN context;
        public WishlistController()
        {
            context = new APP_DATA_DATN();
        }

        [HttpGet("wishlist-get")]
        public IActionResult Get()
        {
            return Ok(context.Wishlist.ToList());
        }

        [HttpGet("wishlist-get-id")]
        public IActionResult Get(long id)
        {
            var wishlistItem = context.Wishlist.Find(id);
            if (wishlistItem == null)
            {
                return NotFound("Wishlist item not found");
            }
            return Ok(wishlistItem);
        }

        [HttpPost("wishlist-post")]
        public ActionResult Post(Wishlist wishlist)
        {
            try
            {
                context.Wishlist.Add(wishlist);
                context.SaveChanges();
                return Ok(new { message = "Thêm vào danh sách yêu thích thành công" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("wishlist-put")]
        public IActionResult Put(Wishlist wishlist)
        {
            var item = context.Wishlist.Find(wishlist.Id);
            if (item == null)
            {
                return NotFound("Wishlist item not found");
            }

            item.User_id = wishlist.User_id;
            item.Create_at = wishlist.Create_at;
            item.Updated_at = wishlist.Updated_at;
            context.SaveChanges();
            return Ok(new { message = "Cập nhật danh sách yêu thích thành công" });
        }

        [HttpDelete("wishlist-delete")]
        public IActionResult Delete(long id)
        {
            var delete = context.Wishlist.Find(id);
            if (delete == null)
            {
                return NotFound("Wishlist item not found");
            }

            context.Remove(delete);
            context.SaveChanges();
            return Ok(new { message = "Xóa khỏi danh sách yêu thích thành công" });
        }
    }
}
