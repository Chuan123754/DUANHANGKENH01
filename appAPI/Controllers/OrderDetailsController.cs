using AppAPI.Repository;
using  appAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsReponsitory _repo;
        public OrderDetailsController()
        {
            _repo  = new OrderDetailsReponsitory();
        }
        [HttpGet("All")]
        public async Task<List<Order_details>> GetAll()
        {
            return await _repo.GetAlldetail();
        }
        [HttpGet("Details")]
        public async Task<Order_details> Details(long id)
        {
            return await _repo.GetByIdOrderdetails(id);
        }

        [HttpPost("Create")]
        public async Task Post(Order_details order_Details)
        {
            await _repo.Create(order_Details);
        }

        [HttpPut("Update")]
        public async Task Put(Order_details order_Details)
        {
            await _repo.Update(order_Details);
        }

        [HttpDelete("Delete")]
        public async Task Delete(long id)
        {
            await _repo.Delete(id);
        }
    }
}
