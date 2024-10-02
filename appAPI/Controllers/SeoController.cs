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
        private readonly SeoReponsitory _repon;
        public SeoController()
        {
            _repon = new SeoReponsitory();
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
