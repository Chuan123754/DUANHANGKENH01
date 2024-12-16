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
        [HttpPost("Post")]
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


        [HttpPut("Processback/{id}")]
        public IActionResult Processback(long id)
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


        [HttpPut("UpdateReturnQuantity/{id}")]
        public IActionResult UpdateReturnQuantity(long id, [FromBody] UpdateReturnQuantityRequest request)
        {
            // Tìm sản phẩm trả lại theo ID
            var productReturned = _repository.Find(pr => pr.Id == id).FirstOrDefault();
            if (productReturned == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm trả lại." });
            }

            // Lấy sản phẩm từ OrderDetail thông qua OrderDetailId
            var orderDetail = _orderDetailsRepository.Find(od => od.Id == productReturned.OrderDetailId).FirstOrDefault();
            if (orderDetail == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin chi tiết hóa đơn." });
            }

            // Lấy sản phẩm từ ProductAttributes
            var productAttribute = _productAttributeRepository.Find(pa => pa.Id == orderDetail.Product_Attribute_Id).FirstOrDefault();
            if (productAttribute == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin sản phẩm." });
            }

            // Kiểm tra số lượng trả lại hợp lệ
            if (request.QuantityToStock > productReturned.Quantity)
            {
                return BadRequest(new { message = "Số lượng đưa lại vào kho vượt quá số lượng đổi trả." });
            }

            // Cộng số lượng vào kho
            productAttribute.Stock += request.QuantityToStock;
            _productAttributeRepository.Update(productAttribute);

            // Cập nhật số lượng còn lại trong sản phẩm đổi trả
            var remainingQuantity = productReturned.Quantity - request.QuantityToStock;
            if (remainingQuantity > 0)
            {
                // Tạo bản ghi mới cho số lượng còn lại với trạng thái "Hỏng"
                var damagedProduct = new Products_Returned
                {
                    OrderDetailId = productReturned.OrderDetailId,
                    Quantity = remainingQuantity,
                    ReturnReason = productReturned.ReturnReason,
                    Condition = "Hỏng",
                    ReturnDate = productReturned.ReturnDate,
                    IsReturn = false,
                    UnitPrice = productReturned.UnitPrice, // Đơn giá từ ProductAttribute
                    Notes = $"Sản phẩm hỏng IDOrder: {productReturned.OrderDetailId}",
                    TotalPrice = remainingQuantity * productReturned.UnitPrice // Tính tổng giá
                };
                _repository.Add(damagedProduct);
            }

            // Cập nhật bản ghi gốc
            productReturned.Quantity = request.QuantityToStock;
            productReturned.Condition = "Nhập kho";
            productReturned.UnitPrice = productReturned.UnitPrice; // Đơn giá từ ProductAttribute
            productReturned.TotalPrice = request.QuantityToStock * productReturned.UnitPrice; // Tính tổng giá
            _repository.Update(productReturned);

            return Ok(new { message = "Cập nhật sản phẩm trả lại và kho thành công." });
        }


        // DTO để nhận dữ liệu từ client
        public class UpdateReturnQuantityRequest
        {
            public int QuantityToStock { get; set; }
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

            // Cập nhật thông tin cơ bản
            existingProductReturned.Quantity = productReturned.Quantity;
            existingProductReturned.ReturnReason = productReturned.ReturnReason;
            existingProductReturned.Condition = productReturned.Condition;
            existingProductReturned.ReturnDate = productReturned.ReturnDate;
            existingProductReturned.UnitPrice = productReturned.UnitPrice;
            existingProductReturned.TotalPrice = productReturned.TotalPrice;
            existingProductReturned.Notes += productReturned.Notes;

            _repository.Update(existingProductReturned);

            return Ok(new { message = "Cập nhật thành công." });
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

            // Lấy sản phẩm từ ProductAttributes thông qua Product_Attribute_Id
            var productAttribute = _productAttributeRepository.Find(pa => pa.Id == orderDetail.Product_Attribute_Id).FirstOrDefault();
            if (productAttribute == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin sản phẩm." });
            }

            // Kiểm tra và cập nhật số lượng tồn kho
            if (productAttribute.Stock >= request.ReturnQuantity)
            {
                productAttribute.Stock -= request.ReturnQuantity;
                _productAttributeRepository.Update(productAttribute); // Cập nhật tồn kho
            }
            else
            {
                return BadRequest(new { message = "Số lượng sản phẩm trả vượt quá tồn kho." });
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


        [HttpPut("ProcessRefundQuantity")]
        public IActionResult ProcessRefundQuantity([FromBody] ProcessReturnRequest request)
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
