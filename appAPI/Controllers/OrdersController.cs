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
        // GET: api/<OrdersController>
        [HttpGet("All-Orders")]
        public async Task<List<Orders>> GetAllOrders()
        {
            return await _repo.GetAll();
        }

        [HttpGet("OrdersDetails")]
        public async Task<Orders> DetailsOrders(long id)
        {
            return await _repo.GetByIdOrders(id);
        }
        // POST api/<OrdersController>
        [HttpPost("Create")]
        public async Task Post(Orders orders)
        {
            await _repo.Create(orders);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("Update")]
        public async Task Put(Orders orders)
        {
            await _repo.Update(orders);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("Delete")]
        public async Task Delete(long id)
        {
            await _repo.Delete(id);
        }
    }
}
