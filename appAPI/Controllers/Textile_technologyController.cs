using appAPI.IRepository;
using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Textile_technologyController : ControllerBase
    {
        private readonly ITextile_technologyReponsitory _repos;
        public Textile_technologyController(ITextile_technologyReponsitory textile_technologyReponsitory)
        {
            _repos = textile_technologyReponsitory;
        }
        // GET: api/<Textile_technologyController>
        [HttpGet]
        public async Task<List<Textile_technology>> GetAll()
        {
            return await _repos.GetAll();
        }

        // GET api/<Textile_technologyController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var style = _repos.GetByIdAndType(id);
            if (style == null)
            {
                return NotFound("Textile_technology not found");
            }
            return Ok(style);
        }

        // POST api/<Textile_technologyController>
        [HttpPost]
        public async Task Post(Textile_technology mate)
        {
            await _repos.Create(mate);
        }

        // PUT api/<Textile_technologyController>/5
        [HttpPut("{id}")]
        public async Task Put(Textile_technology mate)
        {
            await _repos.Update(mate);
        }
        // DELETE api/<Textile_technologyController>/5
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _repos.Delete(id);
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _repos.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repos.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
    }
}
