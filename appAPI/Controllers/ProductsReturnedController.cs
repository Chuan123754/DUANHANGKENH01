using appAPI.IRepository;
using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsReturnedController : ControllerBase
    {
        private readonly IRepository<Products_Returned> _repository;
        private readonly IRepository<Order_details> _orderDetailsRepository;
        private readonly IRepository<Product_Attributes> _productAttributeRepository;

        public ProductsReturnedController(
            IRepository<Products_Returned> repository,
            IRepository<Order_details> orderDetailsRepository,
            IRepository<Product_Attributes> productAttributeRepository
            )
        {
            _repository = repository;
            _orderDetailsRepository = orderDetailsRepository;
            _productAttributeRepository = productAttributeRepository;
        }

        // GET: api/ProductsReturned
        [HttpGet]
        public IActionResult GetAll()
        {
            var productsReturned = _repository.GetAll();
            return Ok(productsReturned);
        }

        // GET: api/ProductsReturned/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var productReturned = _repository.Find(p => p.Id == id,
                p => p.OrderDetails,
                p => p.OrderDetails.Orders,
                p => p.OrderDetails.ProductAttributes).FirstOrDefault();

            if (productReturned == null)
            {
                return NotFound(new { message = "Sản phẩm trả lại không tồn tại." });
            }

            return Ok(productReturned);
        }

        [HttpGet("GetByOrderDetailId")]
        public IActionResult GetByOrderDetailId(long orderDetailId)
        {
            var productsReturned = _repository.Find(p => p.OrderDetailId == orderDetailId).ToList();

            return Ok(productsReturned);
        }


        // POST: api/ProductsReturned
        [HttpPost]
        public IActionResult Create([FromBody] Products_Returned productReturned)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(productReturned);
            return CreatedAtAction(nameof(GetById), new { id = productReturned.Id }, productReturned);
        }


        [HttpPut("ProcessReturn/{id}")]
        public IActionResult ProcessReturn(long id)
        {
            // Tìm bản ghi Products_Returned theo ID
            var productReturned = _repository.Find(pr => pr.Id == id).FirstOrDefault();
            if (productReturned == null)
            {
                return NotFound(new { message = "Không tìm thấy bản ghi đổi trả." });
            }

            // Lấy sản phẩm từ OrderDetail thông qua OrderDetailId
            var orderDetail = _orderDetailsRepository.Find(od => od.Id == productReturned.OrderDetailId).FirstOrDefault();
            if (orderDetail == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin chi tiết hóa đơn." });
            }

            // Lấy sản phẩm từ ProductAttributes thông qua Product_Attribute_Id
            var productAttribute = _productAttributeRepository.Find(pa => pa.Id == orderDetail.Product_Attribute_Id).FirstOrDefault();
            if (productAttribute == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin sản phẩm." });
            }

            // Kiểm tra và cập nhật số lượng tồn kho
            if (productAttribute.Stock >= productReturned.Quantity)
            {
                productAttribute.Stock -= productReturned.Quantity;
                _productAttributeRepository.Update(productAttribute); // Cập nhật tồn kho
            }
            else
            {
                return BadRequest(new { message = "Số lượng sản phẩm trả vượt quá tồn kho." });
            }

            // Cập nhật số lượng trong OrderDetail
            if (orderDetail.Quantity >= productReturned.Quantity)
            {
                orderDetail.Quantity -= productReturned.Quantity;
                _orderDetailsRepository.Update(orderDetail);
            }
            else
            {
                return BadRequest(new { message = "Số lượng sản phẩm trả vượt quá số lượng trong chi tiết hóa đơn." });
            }

            return Ok(new { message = "Cập nhật tồn kho và chi tiết hóa đơn thành công." });
        }




        // PUT: api/ProductsReturned/put
        [HttpPut("put")]
        public IActionResult Update([FromBody] Products_Returned productReturned)
        {
            if (productReturned == null || productReturned.Id <= 0)
            {
                return BadRequest(new { message = "Thông tin sản phẩm trả lại không hợp lệ." });
            }

            var existingProductReturned = _repository.Find(p => p.Id == productReturned.Id).FirstOrDefault();
            if (existingProductReturned == null)
            {
                return NotFound(new { message = "Sản phẩm trả lại không tồn tại." });
            }

            existingProductReturned.Quantity += productReturned.Quantity;
            existingProductReturned.ReturnReason = productReturned.ReturnReason;
            existingProductReturned.Condition = productReturned.Condition;
            existingProductReturned.ReturnDate = productReturned.ReturnDate;
            existingProductReturned.Notes += $"\nThêm số lượng: {productReturned.Quantity}";

            _repository.Update(existingProductReturned);

            return Ok(new { message = "Cập nhật thành công.", data = existingProductReturned });
        }



        [HttpPut("ProcessReturnQuantity")]
        public IActionResult ProcessReturnQuantity([FromBody] ProcessReturnRequest request)
        {
            // Lấy thông tin OrderDetail từ OrderDetailId
            var orderDetail = _orderDetailsRepository.Find(od => od.Id == request.OrderDetailId).FirstOrDefault();
            if (orderDetail == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin chi tiết hóa đơn." });
            }

            // Kiểm tra và cập nhật số lượng
            if (orderDetail.Quantity >= request.ReturnQuantity)
            {
                orderDetail.Quantity -= request.ReturnQuantity;
                _orderDetailsRepository.Update(orderDetail);
            }
            else
            {
                return BadRequest(new { message = "Số lượng sản phẩm trả vượt quá số lượng trong chi tiết hóa đơn." });
            }

            return Ok(new { message = "Cập nhật số lượng sản phẩm trả thành công." });
        }


        // DELETE: api/ProductsReturned/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var productReturned = _repository.GetById(id);
            if (productReturned == null)
            {
                return NotFound(new { message = "Sản phẩm trả lại không tồn tại." });
            }

            _repository.Remove(productReturned);
            return NoContent();
        }
    }

    // Class để nhận dữ liệu từ client
    public class ProcessReturnRequest
    {
        public long OrderDetailId { get; set; }
        public int ReturnQuantity { get; set; }
    }
}
