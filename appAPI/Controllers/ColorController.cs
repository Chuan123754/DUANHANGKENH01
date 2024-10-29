using appAPI.IRepository;
using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorReponsitory _colorrepon;
        public ColorController(IColorReponsitory colorRepository)
        {
            _colorrepon = colorRepository;
        }
        // GET: api/<ColorController>
        [HttpGet]
        public async Task<List<Color>> GetAll()
        {
            return await _colorrepon.GetAll();
        }

        // GET api/<ColorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var colo = _colorrepon.GetByIdAndType(id);
            if (colo == null)
            {
                return NotFound("Color not found");
            }
            return Ok(colo);
        }

        // POST api/<ColorController>
        [HttpPost]
        public async Task Post(Color color)
        {
         
            await _colorrepon.Create(color);
           
        }

        // PUT api/<ColorController>/5
        [HttpPut("{id}")]
        public async Task Put(Color color)
        {
           await _colorrepon.Update(color);
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
           await _colorrepon.Delete(id);    
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _colorrepon.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _colorrepon.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
    }
}
