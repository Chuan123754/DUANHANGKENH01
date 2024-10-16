using appAPI.Models;
using appAPI.Repository;
using AppAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Products> _productRepository;

        public ProductsController(IRepository<Products> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("postproducts-get")]
        public IActionResult Get()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("postproducts-get-id/{id}")]
        public IActionResult Get(long id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost("postproducts-post")]
        public IActionResult Post([FromBody] Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                product.Created_at = DateTime.Now;
                _productRepository.Add(product);
                return Ok(new { message = "Thêm sản phẩm cho bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi thêm sản phẩm", error = ex.Message });
            }
        }

        [HttpPut("postproducts-put")]
        public IActionResult Put([FromBody] Products product)
        {
            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Sku = product.Sku;
            existingProduct.Status = product.Status;
            existingProduct.Deleted_at = product.Deleted_at;
            existingProduct.Created_at = product.Created_at;
            existingProduct.Updated_at = DateTime.Now;
            existingProduct.Post_Id = product.Post_Id;

            try
            {
                _productRepository.Update(existingProduct);
                return Ok(new { message = "Cập nhật sản phẩm cho bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi cập nhật sản phẩm", error = ex.Message });
            }
        }

        [HttpDelete("postproducts-delete/{id}")]
        public IActionResult Delete(long id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            try
            {
                _productRepository.Remove(product);
                return Ok(new { message = "Xóa sản phẩm khỏi bài viết thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi xóa sản phẩm", error = ex.Message });
            }
        }
    }
}
