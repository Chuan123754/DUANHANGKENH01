using appAPI.Models;
using appAPI.IRepository;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderIReponsitory _repo;
        public OrdersController(OrderIReponsitory repon)
        {
            _repo = repon;
        }
        [HttpGet("All-Orders")]
        public async Task<List<Orders>> GetAllOrders()
        {
            return await _repo.GetAll();
        }

        [HttpGet("OrdersDetails")]
        public async Task<IActionResult> DetailsOrders(long id)
        {
            var order = await _repo.GetByIdOrders(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }
            return Ok(order);
        }

        [HttpGet("OrdersAddress")]
        public async Task<IActionResult> OrdersAddress(long id)
        {
            var orderAdd = await _repo.GetByIdOrdersAddress(id);
            if (orderAdd == null)
            {
                return NotFound(new { message = "Order not found" });
            }
            return Ok(orderAdd);
        }

        [HttpGet("GetOrderByIdAdmin")]
        public async Task<IActionResult> GetOrderByIdAdmin(string idAdmin)
        {
            var lstOrder = await _repo.GetOrderByIdAdmin(idAdmin);
            if (lstOrder != null)
            {
                return Ok(lstOrder);
            }
            return NotFound();
        }
        [HttpGet("GetOrderByIdUser")]
        public async Task<IActionResult> GetOrderByIdUser(long idUser)
        {
            var lstOrder = await _repo.GetOrderByIdUser(idUser);
            if (lstOrder != null)
            {
                return Ok(lstOrder);
            }
            return NotFound();
        }
        // POST api/<OrdersController>
        [HttpPost("Create")]
        public async Task<IActionResult> Post(Orders orders)
        {
            try
            {
                await _repo.Create(orders);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<OrdersController>/5
        [HttpPut("UpdateStrees")]
        public async Task<IActionResult> PutStrees(Orders orders,long id)
        {
            try
            {
                await _repo.UpdateSrees(orders, id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<OrdersController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put(Orders orders, long id)
        {
            try
            {
                await _repo.Update(orders, id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> PutStatus(Orders orders, long id)
        {
            try
            {
                await _repo.UpdateStatus(orders, id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpGet("Get-Total-Order")]
        public async Task<IActionResult> GetTotal()
        {
            var totalCount = await _repo.GetTotal();
            return Ok(totalCount);
        }
        [HttpGet("Get-Total-Order-Today")]
        public async Task<IActionResult> GetTotalToday()
        {
            var totalCount = await _repo.GetTotalToday();
            return Ok(totalCount);
        }
        [HttpGet("Get-Total-Pice-Today")]
        public async Task<IActionResult> GetTotalPiceToday()
        {
            var totalCount = await _repo.GetTotalPiceToday();
            return Ok(totalCount);
        }
        [HttpGet("Get-Total-Pice-Week")]
        public async Task<IActionResult> GetTotalPiceWeek()
        {
            var totalCount = await _repo.GetTotalPiceWeek();
            return Ok(totalCount);
        }
        [HttpGet("Get-Total-Pice-Month")]
        public async Task<IActionResult> GetTotalPiceMonth()
        {
            var totalCount = await _repo.GetTotalPiceMonth();
            return Ok(totalCount);
        }
        [HttpGet("Get-Total-Revenue-Per-Year")]
        public async Task<IActionResult> GetTotalRevenuePerYear()
        {

            var revenuePerYear = await _repo.GetTotalRevenuePerYear();

            return Ok(revenuePerYear);
        }
        [HttpGet("Get-Total-Orders-Per-Month")]
        public async Task<IActionResult> GetTotalOrdersPerMonth()
        {
            var totalOrdersPerMonth = await _repo.GetTotalMonth();
            return Ok(totalOrdersPerMonth);
        }


    }
}
