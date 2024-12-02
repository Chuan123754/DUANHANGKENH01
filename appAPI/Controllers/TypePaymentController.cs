using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repo;

        public TypePaymentController(IPaymentRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Payment payment)
        {
            var result = _repo.Create(payment);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
