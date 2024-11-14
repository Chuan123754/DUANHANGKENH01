using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IRepository<Tags> _tagsRepository;

        public TagsController(IRepository<Tags> tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        // GET: api/<TagsController>
        [HttpGet("show")]
        public IActionResult Show()
        {
            try
            {
                var data = _tagsRepository.GetAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error occurred while retrieving tags", error = e.Message });
            }
        }

        // GET api/<TagsController>/5
        [HttpGet("details")]
        public IActionResult Details(long id)
        {
            var data = _tagsRepository.GetById(id);
            if (data == null)
            {
                return NotFound(new { message = "No tag found with id " + id });
            }
            return Ok(data);
        }
        // POST api/<TagsController>
        [HttpPost("add")]
        public IActionResult Add([FromBody] Tags t)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = errors });
            }

            try
            {
                var check = _tagsRepository.Find(p => p.Title == t.Title).FirstOrDefault();
                if (check != null)
                {
                    return BadRequest(new { message = "Tag already exists. Try another title." });
                }

                t.Created_at = DateTime.UtcNow;
                _tagsRepository.Add(t);
                return Ok(new { message = "Tag added successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error occurred while adding tag", error = e.Message });
            }
        }

        // PUT api/<TagsController>/5
        [HttpPut("edit")]
        public IActionResult Edit([FromBody] Tags t)
        {
            var data = _tagsRepository.GetById(t.Id);
            if (data == null)
            {
                return NotFound(new { message = "Tag not found" });
            }

            try
            {
                data.Title = t.Title;
                data.Slug = t.Slug;
                data.Type = t.Type;
                data.Description = t.Description;

                _tagsRepository.Update(data);
                return Ok(new { message = "Tag updated successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error occurred while updating tag", error = e.Message });
            }
        }

        // DELETE api/<TagsController>/5
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var data = _tagsRepository.GetById(id);
            if (data == null)
            {
                return NotFound(new { message = "Tag not found" });
            }

            try
            {
                _tagsRepository.Remove(data);
                return Ok(new { message = "Tag deleted successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error occurred while deleting tag", error = e.Message });
            }
        }
    }
}
