using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepository _repo;

        public BannerController(IBannerRepository repo)
        {
            _repo= repo;
        }
        [HttpGet("GetAllBanner")]
        public async Task<IActionResult> GetAllBanner()
        {
            var result = await _repo.GetAllBanner();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetBannerById")]
        public async Task<IActionResult> GetBannerById(long id)
        {
            var result = await _repo.GetBannerById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost("CreateBanner")]
        public async Task<IActionResult> Create(Banner banner)
        {
            var result = _repo.Create(banner);
            if(result != null)
            {
                 return Ok(result);
            }
            return BadRequest();              
        }
        [HttpPut("UpdateBanner")]
        public async Task<IActionResult> Update(Banner banner, long id)
        {
            var result = _repo.Update(banner, id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteBanner")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = _repo.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
