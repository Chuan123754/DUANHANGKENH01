using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IRepository<Material> _repos;
        public MaterialController(IRepository<Material> materialRepository)
        {
            _repos = materialRepository;
        }
        // GET: api/<MaterialController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repos.GetAll());
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var mate = _repos.GetById(id);
            if (mate == null)
            {
                return NotFound("Material not found");
            }
            return Ok(mate);
        }


        // POST api/<MaterialController>
        [HttpPost]
        public IActionResult Post(Material material)
        {
            try
            {
                material.Create_at = DateTime.Now;
                _repos.Add(material);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Material material)
        {
            var item = _repos.GetById(material.Id);
            if (item == null)
            {
                return BadRequest("Material not found");
            }
            item.Update_at = DateTime.Now;
            item.Title = material.Title;
            item.Description = material.Description;
            item.Slug = material.Slug;
            item.Status = material.Status;
            _repos.Update(item);
            return Ok(new { message = "Cập nhật chất liệu thành công" });
        }
        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _repos.GetById(id);
            if (delete == null)
            {
                return BadRequest("Xoá material thành công");
            }
            delete.Status = "Deleted";
            delete.Delete_at = DateTime.Now;
            _repos.Update(delete);
            return Ok(new { message = "Trạng thái đã được chuyển thành không hoạt động (UNACTIVE)" });
        }
    }
}
