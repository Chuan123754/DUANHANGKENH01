using appAPI.Models;
using appAPI.IRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using appAPI.Repository;

namespace appAPI.Background_Service
{
    public class DiscountStatusChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5); // Tăng khoảng thời gian kiểm tra để giảm tải

        public DiscountStatusChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var discountRepository = scope.ServiceProvider.GetRequiredService<IRepository<Discount>>();
                    var voucherRepository = scope.ServiceProvider.GetRequiredService<IRepository<Vouchers>>();
                    var productAttributesRepository = scope.ServiceProvider.GetRequiredService<IProductAttributesRepository>();
                    var productDiscountRepository = scope.ServiceProvider.GetRequiredService<IProductAttributeDiscountRepository>();

                    try
                    {
                        var today = DateTime.Now;

                        // Lấy các chiết khấu cần cập nhật (lọc trước để giảm dữ liệu)
                        var discounts = discountRepository.GetAll()
                            .Where(d => d.Status != "Đã kết thúc" || (d.Start_date <= today && d.End_date >= today))
                            .ToList();

                        var vouchers = voucherRepository.GetAll()
                             .Where(d => d.Status != "Đã kết thúc" || (d.Start_time <= today && d.End_time >= today))
                             .ToList();

                        var vouchersToUpdate = new List<Vouchers>();
                        var discountsToUpdate = new List<Discount>();
                        var productsToUpdate = new List<Product_Attributes>();

                        foreach (var discount in discounts)
                        {
                            // Cập nhật trạng thái chiết khấu
                            if (discount.End_date >= today && discount.Start_date <= today && discount.Status!="Đã dừng")
                            {
                                discount.Status = "Đang diễn ra";
                            }
                            else if (discount.End_date < today && discount.Status != "Đã dừng")
                            {
                                discount.Status = "Đã kết thúc";
                            }
                            else if (discount.Start_date < today && discount.Status != "Đã dừng")
                            {
                                discount.Status = "Sắp diễn ra";
                            }
                            discountsToUpdate.Add(discount);

                            // Lấy danh sách sản phẩm liên quan
                            var productIds = await productDiscountRepository.GetByIdDiscount(discount.Id);

                            foreach (var productId in productIds)
                            {
                                var product = await productAttributesRepository.GetProductAttributesById(productId.ProductAttributes.Id);
                                if (product != null)
                                {
                                    if (discount.Status == "Đang diễn ra" && discount.Status != "Đã dừng")
                                    {
                                        product.Sale_price = (long?)Math.Round(CalculateSalePrice(
                                            product.Regular_price ?? 0,
                                            discount.Discount_value,
                                            discount.Type_of_promotion
                                        ));
                                    }
                                    else
                                    {
                                        product.Sale_price = null; // Khôi phục giá gốc
                                    }
                                    productsToUpdate.Add(product);
                                }
                            }
                        }
                        // Batch update chiết khấu
                        if (discountsToUpdate.Any())
                        {
                            await discountRepository.BatchUpdateAsync(discountsToUpdate);
                        }
                        // Batch update sản phẩm
                        if (productsToUpdate.Any())
                        {
                            await productAttributesRepository.BatchUpdateAsync(productsToUpdate);
                        }

                        foreach(var voucher in vouchers)
                        {
                            if (voucher.End_time >= today && voucher.Start_time <= today && voucher.Status != "Đã dừng")
                            {
                                voucher.Status = "Đang diễn ra";
                            }
                            else if (voucher.End_time < today && voucher.Status != "Đã dừng")
                            {
                                voucher.Status = "Đã kết thúc";
                            }
                            else if (voucher.Start_time < today && voucher.Status != "Đã dừng")
                            {
                                voucher.Status = "Sắp diễn ra";
                            }
                            voucherRepository.Update(voucher);
                        }    


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                // Tạm dừng trước lần kiểm tra tiếp theo
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private decimal CalculateSalePrice(decimal regularPrice, decimal discountValue, string discountType)
        {
            if (discountType == "Percentage")
            {
                return regularPrice - (regularPrice * discountValue / 100);
            }
            else if (discountType == "Fixed")
            {
                if (discountValue >= regularPrice)
                {
                    return 1000;
                }
                else
                {
                    return regularPrice - discountValue;
                }

            }
            return regularPrice;
        }
    }
}