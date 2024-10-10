using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IRepository<Style> _repos;
        public StyleController(IRepository<Style> styleReponsitory)
        {
            _repos = styleReponsitory;   
        }
        // GET: api/<StyleController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repos.GetAll());
        }

        // GET api/<StyleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var style = _repos.GetById(id);
            if (style == null)
            {
                return NotFound("Style not found");
            }
            return Ok(style);
        }


        // POST api/<StyleController>
        [HttpPost]
        public IActionResult Post(Style style)
        {
            try
            {
                style.Create_at = DateTime.Now;
                _repos.Add(style);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StyleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Style style)
        {
            var item = _repos.GetById(style.Id);
            if (item == null)
            {
                return BadRequest("Size not found");
            }
            item.Update_at = DateTime.Now;
            item.Title = style.Title;
            item.Description = style.Description;
            item.Slug = style.Slug;
            item.Status = style.Status;
            _repos.Update(item);
            return Ok(new { message = "Cập nhật style thành công" });
        }

        // DELETE api/<StyleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _repos.GetById(id);
            if (delete == null)
            {
                return BadRequest("Xoá style thành công");
            }
            delete.Status = "Deleted";
            delete.Delete_at = DateTime.Now;
            _repos.Update(delete);
            return Ok(new { message = "Trạng thái đã được chuyển thành không hoạt động (UNACTIVE)" });
        }
    }
}
