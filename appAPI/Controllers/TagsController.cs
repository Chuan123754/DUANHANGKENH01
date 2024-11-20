using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using appAPI.IRepository;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagsRepository _tagsRepository;

        public TagsController(ITagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }
        [HttpGet("GetTagByPostId")]
        public async Task<IActionResult> GetTagByPostId(long postId)
        {
            try
            {
                var lstTag = await _tagsRepository.GetTagByPostId(postId);
                if (lstTag != null)
                {
                    return Ok(lstTag);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET: api/<TagsController>
        [HttpGet("show")]
        public async Task<IActionResult> Show()
        {
            try
            {
                var data = await _tagsRepository.GetAll();
                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest();
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
            var data = _tagsRepository.Details(id);
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
            try
            {
                _tagsRepository.Create(t);
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
       
            try
            {
                _tagsRepository.Update(t);
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
            var data = _tagsRepository.Delete(id);
            if (data == null)
            {
                return NotFound(new { message = "Tag not found" });
            }

            try
            {
                _tagsRepository.Delete(id);
                return Ok(new { message = "Tag deleted successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error occurred while deleting tag", error = e.Message });
            }
        }
    }
}
