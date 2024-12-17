using appAPI.Repository;
using  appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using appAPI.IRepository;


namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsIReponsitory _repo;
        public OrderDetailsController(OrderDetailsIReponsitory repon)
        {
            _repo  = repon;
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var lstOrderDetails = await _repo.GetAlldetail();
            if (lstOrderDetails != null)
            {
                return Ok(lstOrderDetails);
            }
            return NotFound();
        }
        [HttpGet("GetOrderDetailsByOrderId")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(long idOrder)
        {
            var lstOrderDetails = await _repo.GetOrderDetailsByOrderId(idOrder);
            if (lstOrderDetails != null)
            {
                return Ok(lstOrderDetails);
            }
            return NotFound();
        }
        [HttpGet("GetByOrderIdAndProductAttributeId")]
        public async Task<IActionResult> GetByOrderIdAndProductAttributeId(long orderId, long productAttributeId)
        {
            var result = await _repo.GetByOrderIdAndProductAttributeId(orderId,productAttributeId);
            if (result != null)
            {
                return Ok(result);
            }    
            return NotFound();
        }

        [HttpGet("GetOrderAndReturnedProductsById")]
        public async Task<IActionResult> GetOrderAndReturnedProductsById(long orderId)
        {
            try
            {
                var result = await _repo.GetOrderAndReturnedProductsByIdAsync(orderId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Order or returned products not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Details")]
        public async Task<IActionResult> Details(long id)
        {
            var orderDetail = await _repo.GetByIdOrderdetails(id);
            if (orderDetail != null)
            {
                return Ok(orderDetail);
            }
            return NotFound(); 
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post(Order_details order_Details)
        {
            try
            {
                await _repo.Create(order_Details);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put(Order_details order_Details, long id )
        {
            try
            {
                await _repo.Update(order_Details, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task Delete(long id)
        {
            await _repo.Delete(id);
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _repo.GetByTypeAsync(pageNumber, pageSize);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount()
        {
            var totalCount = await _repo.GetTotalCountAsync();
            return Ok(totalCount);
        }
        // GET: api/Product/TopSelling
        [HttpGet("TopSelling")]
        public async Task<IActionResult> GetTop5SellingProducts()
        {
            var topSellingProducts = await _repo.GetTop5SellingProductsAsync();
            return Ok(topSellingProducts);
        }
    }
}
