using appAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        APP_DATA_DATN context;
        public OptionsController()
        {
            context = new APP_DATA_DATN();
        }
        // GET: api/<OptionsController>
        [HttpGet("show")]
        public ActionResult Show()
        {
            try
            {
                var data = context.Options.ToList();
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }

        // GET api/<OptionsController>/5
        [HttpGet("details")]
        public ActionResult Details(long id)
        {
            var data = context.Options.Find(id);
            if (data == null)
            {
                return NotFound("No result with id " + id);
            }
            return Ok(data);
        }

        // POST api/<OptionsController>
        [HttpPost("add")]
        public ActionResult Add(Options op)
        {
            try
            {
                var check = context.Options.FirstOrDefault(p => p.Optin_name == op.Optin_name);
                if (check != null)
                {
                    return BadRequest("Existed. Try another product or make a quantity edit");
                }
                op.Created_at = DateTime.UtcNow;
                context.Options.Add(op);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }

        // PUT api/<OptionsController>/5
        [HttpPut("edit")]
        public async Task<ActionResult> Edit(Options op)
        {
            try
            {
                var data = await context.Options.FindAsync(op.Id);
                if (data == null)
                {
                    return NotFound("Options not found");
                }
                data.Optin_name = op.Optin_name;
                data.Option_value = op.Option_value;
                data.Created_at = op.Created_at;
                data.Updated_at = DateTime.UtcNow;

                context.Options.Update(data);
                await context.SaveChangesAsync();

                return Ok("Options updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }

        // DELETE api/<OptionsController>/5
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var data = context.Options.Find(id);
                if (data == null)
                {
                    return NotFound("No result founded");
                }
                context.Options.Remove(data);
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
