using AppAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly APP_DATA_DATN context;
        IWareHouseRepository repo;
        public WarehouseController(IWareHouseRepository _repo)
        {
            context = new APP_DATA_DATN();
            repo = _repo;
        }
        // GET: api/<WarehouseController>
        [HttpGet("show")]
        public async Task<ActionResult<List<Warehouse>>> ShowAll()
        {
            try
            {
                return await repo.GetAll();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }

        // GET api/<WarehouseController>/5
        [HttpGet("details")]
        public async Task<ActionResult<Warehouse>> Details(long id)
        {
            try
            {
                var data = await repo.Details(id);
                if (data == null)
                {
                    return NotFound("No result with id: " + id);
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<WarehouseController>
        [HttpPost("add-warehouse")]
        public async Task<ActionResult> Add(Warehouse whs)
        {
            try
            {
                var check = context.Warehouse.FirstOrDefault(p => p.Name == whs.Name);
                if (check != null)
                {
                    return BadRequest("Existed. Try another product or make a quantity edit");
                }
                whs.Created_at = DateTime.UtcNow;
                whs.Updated_at = DateTime.UtcNow;
                await repo.Create(whs);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        [HttpPut("edit-warehouse")]
        public async Task<ActionResult> Edit(Warehouse whs)
        {
            try
            {
                var data = await repo.Details(whs.Id);
                if (data == null)
                {
                    return NotFound("Warehouse not found");
                }
                data.Name = whs.Name;
                data.Address = whs.Address;
                data.PhoneNumber = whs.PhoneNumber;
                data.Status = whs.Status;
                data.Updated_at = DateTime.UtcNow;

                await repo.Update(data);
                return Ok("Warehouse updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }


        // DELETE api/<WarehouseController>/5
        [HttpDelete("delete-warehouse")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var data = await repo.Details(id);
                if (data == null)
                {
                    return NotFound("No product founded");
                }
                await repo.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
    }
}
