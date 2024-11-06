using appAPI.Models;
using appAPI.Models.DTO;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantsDiscountController : ControllerBase
    {
        private readonly IRepository<P_attribute_discount> _pVariantsDiscountRepository;

        public VariantsDiscountController(IRepository<P_attribute_discount> pVariantsDiscountRepository)
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
        public IActionResult Post(PVariantsDiscountDTO dto)
        {
            var variantDiscount = new P_attribute_discount
            {
                P_attribute_Id = dto.P_attribute_Id,  // Chỉ thiết lập ID
                Discount_Id = dto.Discount_Id,      // Chỉ thiết lập ID
                Old_price = dto.Old_price,
                New_price = dto.New_price,
                Status = dto.Status
            };

            _pVariantsDiscountRepository.Add(variantDiscount);
            return Ok(variantDiscount);
        }



        [HttpPut]
        public IActionResult Put([FromBody] P_attribute_discount pVariantDiscount)
        {
            var existing = _pVariantsDiscountRepository.GetById(pVariantDiscount.Id);
            if (existing == null) return NotFound("P_variant discount not found");

            existing.P_attribute_Id = pVariantDiscount.P_attribute_Id;
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
