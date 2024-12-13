//using appAPI.Models;
//using appAPI.IRepository;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using appAPI.Repository;

//namespace appAPI.Background_Service
//{
//    public class DiscountStatusChecker : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

//        public DiscountStatusChecker(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                using (var scope = _serviceProvider.CreateScope())
//                {
//                    var discountRepository = scope.ServiceProvider.GetRequiredService<IRepository<Discount>>();
//                    var productAttributesRepository = scope.ServiceProvider.GetRequiredService<IProductAttributesRepository>();

//                    // Lấy tất cả các chiết khấu
//                    var discounts = discountRepository.GetAll();

//                    foreach (var discount in discounts)
//                    {
//                        // Kiểm tra trạng thái chiết khấu dựa trên ngày kết thúc
//                        if (discount.End_date <= DateTime.Now && discount.Status != "Đã kết thúc")
//                        {
//                            discount.Status = "Đã kết thúc";
//                            discountRepository.Update(discount);

//                            // Cập nhật các `P_attribute_discount` liên quan
//                            foreach (var attributeDiscount in discount.PAttributeDiscounts)
//                            {
//                                if (attributeDiscount.Status != "Đã kết thúc")
//                                {
//                                    attributeDiscount.Status = "Đã kết thúc";

//                                    // Xóa giá bán liên quan đến biến thể sản phẩm
//                                    if (attributeDiscount.ProductAttributes != null)
//                                    {
//                                        attributeDiscount.ProductAttributes.Sale_price = null;
//                                        await productAttributesRepository.Update(attributeDiscount.ProductAttributes, attributeDiscount.ProductAttributes.Id);
//                                    }
//                                }
//                            }
//                        }
//                        else if (discount.Status == "Đang diễn ra")
//                        {
//                            foreach (var attributeDiscount in discount.PAttributeDiscounts)
//                            {
//                                if (attributeDiscount.Status != "Đang diễn ra")
//                                {
//                                    attributeDiscount.Status = "Đang diễn ra";

//                                    // Cập nhật giá bán cho biến thể sản phẩm
//                                    if (attributeDiscount.ProductAttributes != null)
//                                    {
//                                        attributeDiscount.ProductAttributes.Sale_price = CalculateSalePrice(
//                                            attributeDiscount.ProductAttributes.Regular_price,
//                                            discount.Type_of_promotion,
//                                            discount.Discount_value
//                                        );
//                                        await productAttributesRepository.Update(attributeDiscount.ProductAttributes, attributeDiscount.ProductAttributes.Id);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }

//                // Tạm dừng trước lần kiểm tra tiếp theo
//                await Task.Delay(_checkInterval, stoppingToken);
//            }
//        }

//        private long? CalculateSalePrice(long? regularPrice, string? discountType, decimal discountValue)
//        {
//            if (regularPrice == null) return null;

//            return discountType == "Percentage"
//                ? (long)Math.Max(regularPrice.Value - (regularPrice.Value * (decimal)discountValue / 100), 0)
//                : (long)Math.Max(regularPrice.Value - discountValue, 0);
//        }
//    }
//}
