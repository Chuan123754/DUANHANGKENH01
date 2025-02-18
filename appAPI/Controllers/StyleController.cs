﻿using appAPI.IRepository;
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
        private readonly IStyleReponsitory _repos;
        public StyleController(IStyleReponsitory styleReponsitory)
        {
            _repos = styleReponsitory;   
        }
        // GET: api/<StyleController>
        [HttpGet]
        public async Task<List<Style>> GetAll()
        {
            return await _repos.GetAll();
        }


        // GET api/<StyleController>/5
        [HttpGet("{id}")]
        public async Task<Style> Get(long id)
        {
            return await _repos.GetByIdAndType(id);         
        }


        // POST api/<StyleController>
        [HttpPost]
        public async Task Post(Style style)
        {
            await _repos.Create(style);
        }

        // PUT api/<StyleController>/5
        [HttpPut("update-style")]
        public async Task Put(Style style)
        {
            await _repos.Update(style);
        }
        // DELETE api/<StyleController>/5
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _repos.Delete(id);
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _repos.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repos.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
    }
}
