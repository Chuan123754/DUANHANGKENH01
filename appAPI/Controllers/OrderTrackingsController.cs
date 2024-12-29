using appAPI.DTO;
using appAPI.IRepository;
using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace appAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderTrackingsController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IRepository<Orders> _ordersRepository;
        private readonly IRepository<Order_details> _orderDetailsRepository;
        private readonly IRepository<order_trackings> _orderTrackingsRepository;

        public OrderTrackingsController(
            IRepository<Orders> ordersRepository,
            IRepository<Order_details> orderDetailsRepository,
            IRepository<order_trackings> orderTrackingsRepository,
            IAccountRepo accountRepo)
            
            
        {
            _accountRepo = accountRepo;
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _orderTrackingsRepository = orderTrackingsRepository;
        }

        [HttpGet("GetOrderTracking")]
        public async Task<IActionResult> GetOrderTracking(long orderId)
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
                                                            od => od.ProductAttributes.Material,
                                                            od => od.ProductAttributes.Textile_Technology,
                                                            od => od.ProductAttributes.Style,
                                                            od => od.ProductAttributes.Posts).ToList();

            // Lấy thông tin lịch sử trạng thái từ bảng OrderTrackings
            var orderTrackings = _orderTrackingsRepository.Find(ot => ot.OrderId == orderId).ToList();
            foreach (var tracking in orderTrackings)
            {
                if (tracking.Created_by != null)
                {
                    var admin = await _accountRepo.GetAccountById(tracking.Created_by);
                    tracking.Created_by = admin != null ? admin.FirstName + " " + admin.LastName : "";
                }
                else
                {
                    tracking.Created_by = ""; // Nếu không có người tạo
                }
            }

            var dto = new OrderTrackingDTO
            {
                SellerName = orderTrackings.FirstOrDefault()?.Created_by ?? "",
                BuyerName = order.Users?.Name,
                Address = order.Users?.Address,
                Note = order.Note,
                OrderTrackings = orderTrackings.Select(ot => new OrderTrackingItem
                {
                    Id = ot.Id, // Thêm Id của OrderTracking
                    Status = ot.Status,
                    Note = ot.Note,
                    TotalMoney = order.Totalmoney ?? 0,
                    CreatedAt = ot.Created_at ?? DateTime.MinValue,
                    CreateBy = ot.Created_by

                }).ToList(),
                Products = orderDetails.Select(d => new ProductItem
                {
                    SKU = d.ProductAttributes?.SKU,
                    Color = d.ProductAttributes?.Color?.Title,
                    Size = d.ProductAttributes?.Size?.Title,
                    Quantity = d.Quantity,
                    TotalDiscount = d.TotalDiscount ?? 0 , // giá giảm 
                    UnitPrice = d.UnitPrice ?? 0,// giá gốc
                    Image = d.ProductAttributes.Image // Lấy ảnh từ Product_Posts
                }).ToList(),
                TotalPrincipal = order.TotalPrincipal ?? 0,
                TotalAmount = order.TotalAmount ?? 0,
                TotalVoucher = order.TotalVoucher ?? 0,
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
