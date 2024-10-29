using appAPI.IRepository;
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
        private readonly IWishlistReponsitory _repons;
        public WishlistController(IWishlistReponsitory wishlist)
        {
            _repons = wishlist;
        }

        [HttpGet("wishlist-get")]
        public async Task<List<Wishlist>> GetAll()
        {
            return await _repons.GetAll();
        }

        [HttpGet("wishlist-get-id")]
        public async Task<Wishlist> Details(long id)
        {
            return await _repons.GetByIdAndType(id);
        }

        [HttpPost("wishlist-post")]
        public async Task Post(Wishlist list)
        {
            await _repons.Create(list);
        }

        [HttpDelete("wishlist-delete")]
        public async Task Delete(long id)
        {
            await _repons.Delete(id);
        }
    }
}
