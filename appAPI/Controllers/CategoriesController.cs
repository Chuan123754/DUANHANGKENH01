﻿using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoriesRepository _repo;

        public CategoryController(ICategoriesRepository repo, APP_DATA_DATN context)
        {
            _repo = repo;
         
        }

        // GET: api/Category/show
        [HttpGet("show")]
        public async Task<ActionResult<List<Categories>>> GetAll()
        {
            try
            {
                return Ok(await _repo.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        [HttpGet("GetAllType")]
        public async Task<ActionResult<List<Categories>>> GetAllType(string type)
        {
            try
            {
                return Ok(await _repo.GetAllType(type));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        // GET: api/Category/details/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<Categories>> Details(long id)
        {
            try
            {
                var category = await _repo.Details(id);
                if (category == null)
                {
                    return NotFound("No result with id: " + id);
                }
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("GetCategoryByPostId")]
        public async Task<IActionResult> GetCategoryByPostId(long postId)
        {
            try
            {
                var result = await _repo.GetCategoryByPosstId(postId);
                return Ok(result);
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);   
            }
        }
        // POST: api/Category/add-category
        [HttpPost("add-category")]
        public async Task Add(Categories c)
        {
           await _repo.Create(c);
        }
        [HttpPost("add-category-post")]
        public async Task AddCTPost(Categories c)
        { 
             await _repo.CreateTypePost(c);
        }
        [HttpPost("add-category-project")]
        public async Task AddCTProject(Categories c)
        {
           await _repo.CreateTypeProject(c);
        }
        [HttpPost("add-category-product")]
        public async Task AddCTProduct(Categories c)
        {
          await _repo.CreateTypeProduct(c);
        }

        // PUT: api/Category/edit-category
        [HttpPut("edit-category")]
        public async Task<ActionResult> Edit(Categories c)
        {
            try
            {
                var existingCategory = await _repo.Details(c.Id);
                if (existingCategory == null)
                {
                    return NotFound("Category not found");
                }

                existingCategory.Title = c.Title;
                existingCategory.Short_title = c.Description;
                existingCategory.Slug = c.Slug;
                existingCategory.Parent_id = c.Parent_id;
                existingCategory.Dept = c.Dept;
                existingCategory.Description = c.Description;
                existingCategory.Type = c.Type;
                existingCategory.Created_at = c.Created_at;
                existingCategory.Updated_at = DateTime.UtcNow;

                await _repo.Update(existingCategory);
                return Ok("Category updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }

        // DELETE: api/Category/delete-category/5
        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var data = await _repo.Details(id);
                if (data == null)
                {
                    return NotFound("No category found with id: " + id);
                }
                await _repo.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] string type, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }
         
            var list = await _repo.GetByTypeAsync(type, pageNumber, pageSize, searchTerm);

          
            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string type, [FromQuery] string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required.");
            }

            var totalCount = await _repo.GetTotalCountAsync(type, searchTerm);
            return Ok(totalCount);
        }
    }
}
