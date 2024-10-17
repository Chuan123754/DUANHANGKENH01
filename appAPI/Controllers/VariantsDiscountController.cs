using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantsDiscountController : ControllerBase
    {
        private readonly IRepository<P_variants_discount> _pVariantsDiscountRepository;

        public VariantsDiscountController(IRepository<P_variants_discount> pVariantsDiscountRepository)
        {
            _pVariantsDiscountRepository = pVariantsDiscountRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pVariantsDiscountRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var pVariantDiscount = _pVariantsDiscountRepository.GetById(id);
            if (pVariantDiscount == null) return NotFound("P_variant discount not found");
            return Ok(pVariantDiscount);
        }

        [HttpPost]
        public IActionResult Post([FromBody] P_variants_discount pVariantDiscount)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _pVariantsDiscountRepository.Add(pVariantDiscount);
            return Ok("P_variant discount added successfully");
        }

        [HttpPut]
        public IActionResult Put([FromBody] P_variants_discount pVariantDiscount)
        {
            var existing = _pVariantsDiscountRepository.GetById(pVariantDiscount.Id);
            if (existing == null) return NotFound("P_variant discount not found");

            existing.P_variants_Id = pVariantDiscount.P_variants_Id;
            existing.Discount_Id = pVariantDiscount.Discount_Id;
            existing.Old_price = pVariantDiscount.Old_price;
            existing.New_price = pVariantDiscount.New_price;
            existing.Status = pVariantDiscount.Status;

            _pVariantsDiscountRepository.Update(existing);
            return Ok("P_variant discount updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var pVariantDiscount = _pVariantsDiscountRepository.GetById(id);
            if (pVariantDiscount == null) return NotFound("P_variant discount not found");
            _pVariantsDiscountRepository.Remove(pVariantDiscount);
            return Ok("P_variant discount deleted successfully");
        }
    }

}
