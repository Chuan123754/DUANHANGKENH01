using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactReponsetory _repon;
        public ContactController(IContactReponsetory contactReponsetory)
        {
            _repon = contactReponsetory;
        }
        // GET: api/<SizeController>
        [HttpGet("GetAllContact")]
        public async Task<List<Contact>> GetAll()
        {
            return await _repon.GetAll();
        }
        [HttpGet("GetByIdContact")]

        public async Task<Contact> GetById(long id)
        {
            return await _repon.GetById(id);
        }
        [HttpPost("CreateContact")]
        public async Task Post(Contact contact)
        {
            await _repon.Create(contact);
        }
        [HttpDelete("DeleteContact")]
        public async Task Delete(long id)
        {
            await _repon.Delete(id);
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _repon.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repon.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
    }
}
