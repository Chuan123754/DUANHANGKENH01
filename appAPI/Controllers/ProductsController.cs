using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly APP_DATA_DATN context;

        public ProductsController(APP_DATA_DATN context)
        {
            this.context = context;
        }

        [HttpGet("postproducts-get")]
        public IActionResult Get()
        {
            return Ok(context.Products.ToList());
        }

        [HttpGet("postproducts-get-id/{id}")]
        public IActionResult Get(long id)
        {
            var postProduct = context.Products.Find(id);
            if (postProduct == null)
            {
                return NotFound("Post product not found");
            }
            return Ok(postProduct);
        }

        [HttpPost("postproducts-post")]
        public ActionResult Post(Products postProduct)
        {
            try
            {
                postProduct.Created_at = DateTime.Now;
                context.Products.Add(postProduct);
                context.SaveChanges();
                return Ok(new { message = "Thêm sản phẩm cho bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("postproducts-put")]
        public IActionResult Put(Products postProduct)
        {
            var item = context.Products.Find(postProduct.Id);
            if (item == null)
            {
                return NotFound("Post product not found");
            }

            item.Sku = postProduct.Sku;
            item.Status = postProduct.Status;
            item.Deleted_at = null;
            item.Created_at = postProduct.Created_at;
            item.Updated_at = DateTime.Now;
            item.Post_Id = postProduct.Post_Id;

            context.SaveChanges();
            return Ok(new { message = "Cập nhật sản phẩm cho bài viết thành công" });
        }

        [HttpDelete("postproducts-delete/{id}")]
        public IActionResult Delete(long id)
        {
            var delete = context.Products.Find(id);
            if (delete == null)
            {
                return NotFound("Post product not found");
            }

            context.Remove(delete);
            context.SaveChanges();
            return Ok(new { message = "Xóa sản phẩm khỏi bài viết thành công" });
        }
    }
}
