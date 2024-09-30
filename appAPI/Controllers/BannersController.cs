using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        APP_DATA_DATN context;
        public BannersController()
        {
            context = new APP_DATA_DATN();
        }

        [HttpGet("Banners-get")]
        public IActionResult Get()
        {
            return Ok(context.Banner.ToList());
        }

        [HttpGet("Banners-get-id")]
        public IActionResult Get(long id)
        {
            var banner = context.Banner.Find(id);
            if (banner == null)
            {
                return NotFound("Banner not found");
            }
            return Ok(banner);
        }

        [HttpPost("Banners-post")]
        public ActionResult Post(Banner banner)
        {
            try
            {
                banner.Created_at = DateTime.Now;
                context.Banner.Add(banner);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("Banners-put")]
        public IActionResult Put(Banner banner)
        {
            var item = context.Banner.Find(banner.Id);
            if (item == null)
            {
                return NotFound("Banner not found");
            }

            item.Name = banner.Name;
            item.Type = banner.Type;
            item.Status = banner.Status;
            item.Meta_data = banner.Meta_data;
            item.Created_by = banner.Created_by;
            item.Updated_by = banner.Updated_by;
            item.Created_at = banner.Created_at;
            item.Updated_at = DateTime.Now;
            context.SaveChanges();
            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpDelete("Banners-delete")]
        public IActionResult Delete(long id)
        {
            var delete = context.Banner.Find(id);
            if (delete == null)
            {
                return NotFound("Banner not found");
            }

            context.Remove(delete);
            context.SaveChanges();
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
