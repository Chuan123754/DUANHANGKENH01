using appAPI.IRepository;
using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeoController : ControllerBase
    {
        private readonly SeoIReponsitory _repon;
        public SeoController(SeoIReponsitory repon)
        {
            _repon = repon;
        }
        [HttpGet("Get-id")]
        public async Task<Seo> GetById(long id)
        {
            return await _repon.GetById(id);
        }
        [HttpPost("Create")]
        public async Task Create(Seo seo)
        {
            await _repon.CreateSeo(seo);
        }

        [HttpPut("Update")]
        public async Task Update(Seo seo)
        {
            await _repon.UpdateSeo(seo);
        }
    }
}
