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

        [HttpGet("GetBannerByProductPostId")]
        public async Task<IActionResult> GetBannerByProductPostId(long PostId)
        {
            var result = await _repo.GetBannerByProductPostId(PostId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("CreateBanner")]
        public async Task Create(Banner banner)
        {
            await _repo.Create(banner);             
        }
        [HttpPost("CreateBannerPost")]
        public async Task CreateBannerPosst(long postId, Banner banner)
        {
            await _repo.AddBannerToPost(postId ,banner);      
        }


        [HttpPut("UpdateBanner")]
        public async Task Update(Banner banner, long postId)
        {
            await _repo.Update(banner, postId);
          
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
