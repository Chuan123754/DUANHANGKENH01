using appAPI.DTO;
using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderTrackingsController : ControllerBase
    {
        private readonly IRepository<Orders> _ordersRepository;
        private readonly IRepository<Order_details> _orderDetailsRepository;
        private readonly IRepository<order_trackings> _orderTrackingsRepository;

        public OrderTrackingsController(
            IRepository<Orders> ordersRepository,
            IRepository<Order_details> orderDetailsRepository,
            IRepository<order_trackings> orderTrackingsRepository)
        {
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _orderTrackingsRepository = orderTrackingsRepository;
        }

        [HttpGet("GetOrderTracking")]
        public IActionResult GetOrderTracking(long orderId)
        {
            // Lấy thông tin đơn hàng
            var order = _ordersRepository.Find(o => o.Id == orderId,
                                               o => o.Admin,
                                               o => o.Users,
                                               o => o.Address).FirstOrDefault();


            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng.");
            }

            // Lấy thông tin chi tiết đơn hàng và bao gồm ProductAttributes
            var orderDetails = _orderDetailsRepository.Find(od => od.OrderId == orderId,
                                                            od => od.ProductAttributes,
                                                            od => od.ProductAttributes.Color,
                                                            od => od.ProductAttributes.Size,
                                                            od => od.ProductAttributes.Product_Variant.Posts).ToList();

            // Lấy thông tin lịch sử trạng thái từ bảng OrderTrackings
            var orderTrackings = _orderTrackingsRepository.Find(ot => ot.OrderId == orderId).ToList();

            var dto = new OrderTrackingDTO
            {
                SellerName = order.CreatedByAdminId,
                BuyerName = order.Users?.Name,
                Address = order.Users?.Address,
                OrderTrackings = orderTrackings.Select(ot => new OrderTrackingItem
                {
                    Id = ot.Id, // Thêm Id của OrderTracking
                    Status = ot.Status,
                    Note = ot.Note,
                    TotalMoney = order.Totalmoney ?? 0,
                    CreatedAt = ot.Created_at ?? DateTime.MinValue
                }).ToList(),
                Products = orderDetails.Select(d => new ProductItem
                {
                    SKU = d.ProductAttributes?.SKU,
                    Color = d.ProductAttributes?.Color?.Title,
                    Size = d.ProductAttributes?.Size?.Title,
                    Quantity = d.Quantity,
                    UnitPrice = d.ProductAttributes?.Sale_price ?? 0 , // giá giảm 
                    Regular_price = d.ProductAttributes?.Regular_price ?? 0,// giá gốc
                    Image_library = d.ProductAttributes?.Product_Variant?.Posts?.Image_library // Lấy ảnh từ Product_Posts
                }).ToList(),
                TotalPrincipal = order.TotalPrincipal ?? 0,
                TotalAmount = order.TotalAmount ?? 0,
                TypePayment=order.TypePayment,
                TotalDiscount = (order.TotalPrincipal ?? 0) - (order.TotalAmount ?? 0),
                FeeShipping = order.FeeShipping ?? 0,
                TotalMoney = order.Totalmoney ?? 0
            };

            return Ok(dto);
        }
        
        [HttpPost("AddOrderTracking")]
        public IActionResult AddOrderTracking(order_trackings dto)
        {
            if (dto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var orderTracking = new order_trackings
            {
                OrderId = dto.OrderId,
                Note = dto.Note,
                Created_at = DateTime.Now,
                Created_by = dto.Created_by,
                Status = dto.Status
            };

            _orderTrackingsRepository.Add(orderTracking);
            return Ok("Thêm mới thành công.");
        }

        [HttpPut("UpdateOrderTracking/{id}")]
        public IActionResult UpdateOrderTracking(long id, order_trackings dto)
        {
            if (dto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var orderTracking = _orderTrackingsRepository.Find(o => o.Id == id).FirstOrDefault();
            if (orderTracking == null)
            {
                return NotFound("Không tìm thấy order tracking để cập nhật.");
            }

            orderTracking.Note = dto.Note;
            orderTracking.Status = dto.Status;
            orderTracking.Updated_at = DateTime.Now;

            _orderTrackingsRepository.Update(orderTracking);
            return Ok("Cập nhật thành công.");
        }

        [HttpDelete("DeleteOrderTracking/{id}")]
        public IActionResult DeleteOrderTracking(long id)
        {
            var orderTracking = _orderTrackingsRepository.Find(o => o.Id == id).FirstOrDefault();
            if (orderTracking == null)
            {
                return NotFound("Không tìm thấy order tracking để xóa.");
            }

            _orderTrackingsRepository.Remove(orderTracking);
            return Ok("Xóa thành công.");
        }

    }
}
