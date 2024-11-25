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
        private readonly IRepository<Product_Attributes> _Productvariants;
        private readonly IRepository<Discount> _Discount;

        public VariantsDiscountController(
            IRepository<P_attribute_discount> pVariantsDiscountRepository,
            IRepository<Product_Attributes> productVariantsRepository,
            IRepository<Discount> discountRepository)
        {
            _pVariantsDiscountRepository = pVariantsDiscountRepository;
            _Productvariants = productVariantsRepository;
            _Discount = discountRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pVariantsDiscountRepository.GetAll());
        }

        [HttpGet("GetProductvariants")]
        public IActionResult GetProductvariants()
        {
            return Ok(_Productvariants.GetAll());
        }

        [HttpGet("GetDiscount")]
        public IActionResult GetDiscount()
        {
            return Ok(_Discount.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var pVariantDiscount = _pVariantsDiscountRepository.GetById(id);
            if (pVariantDiscount == null) return NotFound("P_variant discount not found");
            return Ok(pVariantDiscount);
        }

        [HttpPost]
        public IActionResult Post(PattributesDiscountDTO dto)
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
        public IActionResult Put(PattributesDiscountDTO dto)
        {
            var existing = _pVariantsDiscountRepository.GetById(dto.Id);
            if (existing == null) return NotFound("P_variant discount not found");

            existing.P_attribute_Id = dto.P_attribute_Id;
            existing.Discount_Id = dto.Discount_Id;
            existing.Old_price = dto.Old_price;
            existing.New_price = dto.New_price;
            existing.Status = dto.Status;

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
