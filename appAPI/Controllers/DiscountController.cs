using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IRepository<Discount> _discountRepository;

        public DiscountController(IRepository<Discount> discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_discountRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var discount = _discountRepository.GetById(id);
            if (discount == null) return NotFound("Discount not found");
            return Ok(discount);
        }

        [HttpPost("post")]
        public IActionResult Post(Discount discount)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _discountRepository.Add(discount);
            return Ok(discount);
        }
        
        [HttpPut("put")]
        public IActionResult Put(Discount discount)
        {
            var existing = _discountRepository.GetById(discount.Id);
            if (existing == null) return NotFound("Discount not found");
            existing.Code = discount.Code;
            existing.Name = discount.Name;
            existing.Type_of_promotion = discount.Type_of_promotion;
            existing.Discount_value = discount.Discount_value;
            existing.IsGlobal = discount.IsGlobal;
            existing.Start_date = discount.Start_date;
            existing.End_date = discount.End_date;
            existing.Status = discount.Status;
            _discountRepository.Update(existing);
            return Ok("Discount updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var discount = _discountRepository.GetById(id);
            if (discount == null) return NotFound("Discount not found");
            _discountRepository.Remove(discount);
            return Ok("Discount deleted successfully");
        }
    }

}
