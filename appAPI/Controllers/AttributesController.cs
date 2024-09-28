using appAPI.Models;
using appAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using appAPI.Repository;
using appAPI.IRepository;


namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly AttributesRepon _repons;
        public AttributesController()
        {
            _repons = new AttributesRepon();
        }
 
        [HttpGet("Attributes-get")]

        public async Task<List<Attributes>> Get()
        {
            return await _repons.GetAll();
        }

        [HttpGet("Attributes-get-id")]

        public async Task<Attributes> Get(long id)
        {
            return await _repons.GetById(id);
        }

        [HttpPost("Attributes-post")]
        public async Task Post(Attributes attribute)
        {
            await _repons.Create(attribute);
        }

        [HttpPut("Attributes-put")]
        public async Task Put(Attributes attribute)
        {
            await _repons.Update(attribute);
        }

        [HttpDelete("Attributes-delete")]
        public async Task Delete(long id)
        {
            await _repons.Delete(id);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var result = await _repons.Search(query);
            return Ok(result);
        }
    }
}
