 using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class OrderDetailsReponsitory : OrderDetailsIReponsitory
    {
        private readonly APP_DATA_DATN _context;
        public OrderDetailsReponsitory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task<Order_details> GetByOrderIdAndProductAttributeId(long orderId, long productAttributeId)
        {
            return await _context.Order_Details
                                 .FirstOrDefaultAsync(od => od.OrderId == orderId && od.Product_Attribute_Id == productAttributeId);
        }

        public async Task Create(Order_details orderdetails)
        {
           _context.Order_Details.Add(orderdetails);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var itemdelete = await GetByIdOrderdetails(id);
            _context.Order_Details.Remove(itemdelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order_details>> GetAlldetail()
        {
           return await _context.Order_Details
                .Include(o=>o.ProductAttributes)
                .ToListAsync();
        }

        public async Task<Order_details> GetByIdOrderdetails(long id)
        {
            return await _context.Order_Details.FindAsync(id);
        }
        public async Task<List<Order_details>> GetOrderDetailsByOrderId(long idOrder)
        {
            return await _context.Order_Details
                .Where(o=>o.OrderId == idOrder)
                .Include(o => o.Orders).ThenInclude(od => od.Users)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Color)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Size)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Material)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Textile_Technology)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Style)
                .Include(o=>o.ProductAttributes).ThenInclude(o=>o.Posts)
                .Include(o=>o.Orders)
                .ToListAsync();
        }

        public async Task<Order_details> GetOrderAndReturnedProductsByIdAsync(long orderDetailId)
        {
            var orderDetails = await _context.Order_Details
                .Where(od => od.Id == orderDetailId) // Tìm theo Id của Order_Details
                .Include(od => od.Orders) // Bao gồm thông tin Orders
                .Include(od => od.ProductAttributes) // Bao gồm thông tin ProductAttributes
                .ThenInclude(pa => pa.Color) // Nếu cần thông tin Color
                .Include(od => od.ProductAttributes)
                .ThenInclude(pa => pa.Size) // Nếu cần thông tin Size
                .FirstOrDefaultAsync(); // Trả về một kết quả duy nhất hoặc null

            // Kiểm tra nếu không tìm thấy
            if (orderDetails == null)
            {
                throw new KeyNotFoundException($"OrderDetail with ID {orderDetailId} not found.");
            }

            return orderDetails;
        }




        public async Task Update(Order_details orderdetails, long id)
        {
            var updateItem = await GetByIdOrderdetails(id);
            if (updateItem != null)
            {
                updateItem.Quantity = orderdetails.Quantity;
                updateItem.UnitPrice = orderdetails.UnitPrice;
                updateItem.TotalDiscount = orderdetails.TotalDiscount;
                _context.Order_Details.Update(updateItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order_details>> GetByTypeAsync(int pageNumber, int pageSize)
        {
            return await _context.Order_Details            
               .OrderBy(p => p.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Order_Details.CountAsync();
        }

        public async Task<List<TopSellingProductDto>> GetTop5SellingProductsAsync()
        {
            var topSellingProducts = await _context.Order_Details
                 .Where(p => p.Orders.Status != "Hóa đơn treo" && p.Orders.Status != "Chờ xác nhận" && p.Orders.Status != "Đơn huỷ" && p.Orders.Status != "Giao thất bại" && p.Orders.Status != "Pending")
                .GroupBy(od => od.ProductAttributes.Post_Id) // Nhóm theo Post_Id của Product_Variant
                .Select(group => new
                {
                    PostId = group.Key,
                    TotalQuantitySold = group.Sum(od => od.Quantity) // Tổng số lượng bán của từng Post_Id
                })
               
                .OrderByDescending(p => p.TotalQuantitySold) // Sắp xếp theo số lượng bán giảm dần
                .Take(5) // Lấy 5 sản phẩm bán chạy nhất
                .Join(_context.Posts, // Kết hợp với bảng Posts để lấy tên sản phẩm
                    sales => sales.PostId,
                    post => post.Id,
                    (sales, post) => new TopSellingProductDto
                    {
                        ProductName = post.Title, // Lấy tên sản phẩm từ bảng Posts
                        TotalQuantitySold = sales.TotalQuantitySold // Tổng số lượng bán
                    })
                .ToListAsync();

            return topSellingProducts;
        }

        public class TopSellingProductDto
        {
            public string ProductName { get; set; } // Tên sản phẩm
            public int TotalQuantitySold { get; set; } // Tổng số lượng bán
        }

    }
}
