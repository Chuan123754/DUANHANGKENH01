using AppAPI.IRepository;
using AppAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
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
