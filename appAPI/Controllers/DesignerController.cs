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
        // GET: api/Designer
        [HttpGet]
        public async Task<List<Designer>> GetAll()
        {
            return await _repon.GetAll();
        }

        // GET: api/Designer/{id}
        [HttpGet("{id}")]
        public async Task<Designer> GetById(long id)
        {
            return await _repon.GetById(id);
        }


        // POST: api/Designer
        [HttpPost]
        public async Task Post(Designer d)
        {
            await _repon.Create(d);
        }

        // PUT: api/Designer/{id}
        [HttpPut("{id}")]
        public async Task Put(long id, Designer d)
        {
            //d.Id = id; // Đảm bảo đối tượng `d` có `id` cần cập nhật
            await _repon.Update(d);
        }

        // DELETE: api/Designer/{id}
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _repon.Delete(id);
        }

    }
}
