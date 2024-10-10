using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IRepository<Size> _repos;
        public SizeController(IRepository<Size> sizeReponsitory)
        {
            _repos = sizeReponsitory;
        }
        // GET: api/<SizeController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repos.GetAll());
        }

        // GET api/<SizeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var size = _repos.GetById(id);
            if (size == null)
            {
                return NotFound("Size not found");
            }
            return Ok(size);
        }

        // POST api/<SizeController>
        [HttpPost]
        public IActionResult Post(Size size)
        {
            try
            {
                size.Create_at = DateTime.Now;
                _repos.Add(size);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<SizeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Size size)
        {
            var item = _repos.GetById(size.Id);
            if (item == null)
            {
                return BadRequest("Size not found");
            }
            item.Update_at = DateTime.Now;
            item.Title = size.Title;
            item.Description = size.Description;
            item.Slug = size.Slug;
            item.Status = Size.STATUS_ACTIVE;
            _repos.Update(item);
            return Ok(new { message = "Cập nhật size thành công" });
        }

        // DELETE api/<SizeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var delete = _repos.GetById(id);
            if (delete == null)
            {
                return BadRequest("Xoá size thành công");
            }
            delete.Status = "Deleted";
            delete.Delete_at = DateTime.Now;
            _repos.Update(delete);
            return Ok(new { message = "Trạng thái đã được chuyển thành không hoạt động (UNACTIVE)" });
        }
    }
}
