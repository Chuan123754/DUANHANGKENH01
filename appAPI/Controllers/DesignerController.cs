using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignerController : ControllerBase
    {
        private readonly DesignerRepon _repon;
        public DesignerController()
        {
            _repon = new DesignerRepon();
        }
        // GET: api/<DesignerController>
        [HttpGet]
        public async Task<List<Designer>> GetAll()
        {
            return await _repon.GetAll();
        }

        // GET api/<DesignerController>/5
        [HttpGet]
        public async Task<Designer> GetById(long id)
        {
            return await _repon.GetById(id);
        }

        // POST api/<DesignerController>
        [HttpPost]
        public async Task Post(Designer d)
        {
            await _repon.Create(d);
        }

        // PUT api/<DesignerController>/5
        [HttpPut]
        public async Task Put(Designer d)
        {
            await _repon.Update(d);
        }

        // DELETE api/<DesignerController>/5
        [HttpDelete]
        public async Task Delete(long id)
        {
            await _repon.Delete(id);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var relut = await _repon.Search(query);
            return Ok(relut);
        }
    }
}
