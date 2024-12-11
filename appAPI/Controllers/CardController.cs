using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IRepository<Carts> _cartRepository;
        private readonly IRepository<Users> _userRepository;
        
        public CartsController(IRepository<Carts> cartRepository, IRepository<Users> userRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        [HttpGet("carts-get")]
        public IActionResult Get()
        {
            var carts = _cartRepository.GetAll()
                .AsQueryable()
                .Include(c => c.Cart_Details)
                .Include(c => c.Users)
                .ToList();
            return Ok(carts);
        }

        [HttpGet("carts-get-id/{id}")]
        public IActionResult Get(long id)
        {
            var cart = _cartRepository.GetAll()
                .AsQueryable()
                .Include(c => c.Cart_Details)
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == id);

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            return Ok(cart);
        }


        [HttpPost("carts-post")]
        public IActionResult Post(Carts cart)
        {
            try
            {
                var existingUser = _userRepository.GetById(cart.UserId??0); // Sử dụng UserId thay vì đối tượng Users
                if (existingUser == null)
                {
                    return NotFound(new { message = "Người dùng không tồn tại" });
                }

                _cartRepository.Add(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // PUT: api/carts-put
        [HttpPut("carts-put")]
        public IActionResult Put(Carts cart)
        {
            var item = _cartRepository.GetById(cart.Id);
            if (item == null)
            {
                return NotFound("Cart not found");
            }

            item.Status = cart.Status;
            item.Description = cart.Description;

            var existingUser = _userRepository.GetById(cart.Users.Id);
            if (existingUser != null)
            {
                item.Users = existingUser;
            }

            _cartRepository.Update(item);
            return Ok();
        }

        // DELETE: api/carts-delete/{id}
        [HttpDelete("carts-delete/{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _cartRepository.GetById(id);
            if (delete == null)
            {
                return NotFound("Cart not found");
            }

            _cartRepository.Remove(delete);
            return Ok(new { message = "Xóa giỏ hàng thành công" });
        }
    }
}
