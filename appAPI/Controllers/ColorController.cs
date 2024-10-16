using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IRepository<Color> _repos;
        public ColorController(IRepository<Color> colorRepository)
        {
            _repos = colorRepository;
        }
        // GET: api/<ColorController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repos.GetAll());
        }

        // GET api/<ColorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var colo = _repos.GetById(id);
            if (colo == null)
            {
                return NotFound("Color not found");
            }
            return Ok(colo);
        }

        // POST api/<ColorController>
        [HttpPost]
        public IActionResult Post(Color color)
        {
            try
            {
                color.Create_at = DateTime.Now;
                _repos.Add(color);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ColorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Color color)
        {
            var item = _repos.GetById(color.Id);
            if (item == null)
            {
                return BadRequest("Color not found");
            }
            item.Update_at = DateTime.Now;
            item.Title = color.Title;   
            item.Description = color.Description;   
            item.Slug = color.Slug;
            item.Status = color.Status;
            _repos.Update(item);
            return Ok(new { message = "Cập nhật màu sắc thành công" });
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _repos.GetById(id);
            if(delete == null)
            {
                return BadRequest("Xoá màu thành công");
            }
            delete.Status = "Deleted";
            delete.Delete_at = DateTime.Now;
            _repos.Update(delete);
            return Ok(new { message = "Trạng thái đã được chuyển thành không hoạt động (UNACTIVE)" });
        }
    }
}
