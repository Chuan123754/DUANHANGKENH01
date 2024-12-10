using Microsoft.AspNetCore.Mvc;
using appAPI.Models;
using appAPI.IRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _adminRepo;

        public AdminController(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        // GET: api/admin  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            var admins = await _adminRepo.GetAll();
            if(admins == null)
            {
                return NotFound();

            }
             return Ok(admins);
        }

        // GET: api/admin/{id}  
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdminById(long id)
        {
            var admin = await _adminRepo.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Admin admin)
        {
            var result = _adminRepo.Create(admin);
            if (result == null)
            {
                return BadRequest("Admin data is null.");
            }
            return Ok();
        }
        // POST: api/admin/signup  
        [HttpPost("signup")]
        public async Task<ActionResult> Signup([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest("Admin data is null.");
            }

            await _adminRepo.Signup(admin);
            return CreatedAtAction(nameof(GetAdminById), new { id = admin.Id }, admin);
        }

        // POST: api/admin/signin  
        [HttpPost("signin")]
        public async Task<ActionResult<Admin>> Signin([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest("Admin data is null.");
            }

            var result = await _adminRepo.Signin(admin);
            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        // PUT: api/admin/{id}  
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAdmin(long id, [FromBody] Admin admin)
        {
            if (admin == null || id != admin.Id)
            {
                return BadRequest("Admin data is invalid.");
            }

            try
            {
                await _adminRepo.Update(admin, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _adminRepo.GetById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/admin/{id}  
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdmin(long id)
        {
            var existingAdmin = await _adminRepo.GetById(id);
            if (existingAdmin == null)
            {
                return NotFound();
            }

            await _adminRepo.Delete(id);
            return NoContent();
        }
    }
}