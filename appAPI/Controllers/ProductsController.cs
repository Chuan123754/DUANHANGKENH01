//using appAPI.Models;
//using appAPI.Repository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Linq;

//namespace appAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductsController : ControllerBase
//    {
//        private readonly IRepository<Posts> _postRepository;
//        private readonly IRepository<Post_metas> _postMetaRepository;
//        private readonly IRepository<Product_variants> _postProductRepository;
//        private readonly IRepository<Post_tags> _postTagRepository;
//        private readonly IRepository<Post_categories> _postcategorieRepository;
//        private readonly IRepository<Categories> _categoryRepository;
//        private readonly IRepository<Tags> _tagRepository;

//        public ProductsController(IRepository<Posts> postRepository,
//            IRepository<Post_metas> postMetaRepository,
//            IRepository<Product_variants> postProductRepository,
//            IRepository<Post_tags> postTagRepository,
//            IRepository<Post_categories> postcategorieRepository,
//            IRepository<Categories> categoryRepository,
//            IRepository<Tags> tagRepository)
//        {
//            _postRepository = postRepository;
//            _postMetaRepository = postMetaRepository;
//            _postProductRepository = postProductRepository;
//            _postTagRepository = postTagRepository;
//            _postcategorieRepository = postcategorieRepository;
//            _categoryRepository = categoryRepository;
//            _tagRepository = tagRepository;
//        }

//        [HttpGet("product-get")]
//        public IActionResult Get()
//        {
//            return Ok(_postRepository.GetAll().Where(p => p.Type == "product").ToList());
//        }

//        [HttpGet("product-getbyid")]
//        public IActionResult Get(long id)
//        {
//            var postProduct = _postRepository.GetById(id);

//            if (postProduct == null || postProduct.Type != "product")
//            {
//                return NotFound("Post product not found or is not of type 'product'");
//            }

//            return Ok(postProduct);
//        }

//        [HttpPost("postproducts-post")]
//        public ActionResult Post(Posts postProduct)
//        {
//            try
//            {
//                postProduct.Type = "product";
//                postProduct.Created_at = DateTime.Now;
//                _postRepository.Add(postProduct);
                
//                return Ok(new { message = "Thêm sản phẩm cho bài viết thành công" });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPut("postproducts-put")]
//        public IActionResult Put(Product_variants postProduct)
//        {
//            var item = context.product_variants.Find(postProduct.Id);
//            if (item == null)
//            {
//                return NotFound("Post product not found");
//            }

//            item.Sku = postProduct.Sku;
//            item.Status = postProduct.Status;
//            item.Deleted_at = null;
//            item.Created_at = postProduct.Created_at;
//            item.Updated_at = DateTime.Now;
//            item.Post_Id = postProduct.Post_Id;

//            context.SaveChanges();
//            return Ok(new { message = "Cập nhật sản phẩm cho bài viết thành công" });
//        }

//        [HttpDelete("postproducts-delete/{id}")]
//        public IActionResult Delete(long id)
//        {
//            var delete = context.product_variants.Find(id);
//            if (delete == null)
//            {
//                return NotFound("Post product not found");
//            }

//            context.Remove(delete);
//            context.SaveChanges();
//            return Ok(new { message = "Xóa sản phẩm khỏi bài viết thành công" });
//        }
//    }
//}
