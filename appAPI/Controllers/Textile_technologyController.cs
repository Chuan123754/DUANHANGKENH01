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
        IRepository<Textile_technology> _repos;
        public Textile_technologyController(IRepository<Textile_technology> textile_technologyReponsitory)
        {
            _repos = textile_technologyReponsitory;
        }
        // GET: api/<Textile_technologyController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repos.GetAll());
        }


        // GET api/<Textile_technologyController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var style = _repos.GetById(id);
            if (style == null)
            {
                return NotFound("Textile_technology not found");
            }
            return Ok(style);
        }

        // POST api/<Textile_technologyController>
        [HttpPost]
        public IActionResult Post(Textile_technology technology)
        {
            try
            {
                technology.Create_at = DateTime.Now;
                _repos.Add(technology);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<Textile_technologyController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Textile_technology technology)
        {
            var item = _repos.GetById(technology.Id);
            if (item == null)
            {
                return BadRequest("Textile_technology not found");
            }
            item.Update_at = DateTime.Now;
            item.Title = technology.Title;
            item.Description = technology.Description;
            item.Slug = technology.Slug;
            item.Status = Textile_technology.STATUS_ACTIVE;
            _repos.Update(item);
            return Ok(new { message = "Cập nhật textile_technology thành công" });
        }

        // DELETE api/<Textile_technologyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _repos.GetById(id);
            if (delete == null)
            {
                return BadRequest("Xoá textile_technology thành công");
            }
            delete.Status = "Deleted";
            delete.Delete_at = DateTime.Now;
            _repos.Update(delete);
            return Ok(new { message = "Trạng thái đã được chuyển thành không hoạt động (UNACTIVE)" });
        }
    }
}
