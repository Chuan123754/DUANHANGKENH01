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
            var lstOrderDetails = await _repo.GetAlldetail();
            if (lstOrderDetails != null)
            {
                return Ok(lstOrderDetails);
            }
            return NotFound();
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
    }
}
