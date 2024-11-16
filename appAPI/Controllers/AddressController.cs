using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _repo;

        public AddressController(IAddressRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllAddress")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetAddressById")]
        public async Task<IActionResult> GetAddressById(long id)
        {
            var result = await _repo.GetAddressById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("GetAddressByUserId")]
        public async Task<IActionResult> GetAddressByUserId(long userId)
        {
            var result = await _repo.GetAddressByUserId(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost("CreateAddress")]
        public async Task<IActionResult> Create(Address address)
        {
            try
            {
                await _repo.CreateAddress(address);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> Update(long id, Address address)
        {
            var result = _repo.UpdateAddress(address, id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut("SetAsDefault")]
        public async Task<IActionResult> SetAsDefault(long id , Address address)
        {
            var result = _repo.SetAsDefault(id , address);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = _repo.DeleteAddress(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }

}
