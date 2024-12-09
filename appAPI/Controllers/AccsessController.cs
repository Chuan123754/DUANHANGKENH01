using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccsessController : ControllerBase
    {
        private readonly IAccsessViewsRepon _repo;
        public AccsessController(IAccsessViewsRepon repn)
        {
            _repo = repn;
        }
        [HttpGet("GetAll")]
        public async Task GetAll()
        {
           await _repo.CountViewsAccsess();         
        }
        [HttpGet("GetTotal")]
        public async Task<IActionResult> GetTotal()
        {
            var total = await _repo.GetTotal();
            return Ok(total);
        }
    }
}
