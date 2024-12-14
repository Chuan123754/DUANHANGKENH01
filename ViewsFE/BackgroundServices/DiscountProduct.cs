using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.BackgroundServices
{
    public class DiscountProduct : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DiscountProduct(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var discountService = scope.ServiceProvider.GetRequiredService<IDiscountServices>();
                    var productService = scope.ServiceProvider.GetRequiredService<IProductAttributeServices>();
                    var productDiscountService = scope.ServiceProvider.GetRequiredService<IAttributesDiscountServices>();

                    var discounts = await productDiscountService.GetAll();
                    var today = DateTime.Now;

                    var tasks = discounts.Select(async discount =>
                    {
                        if (discount?.Discount != null)
                        {
                            var discountInfo = discount.Discount;
                            if (discountInfo.Start_date != null && discountInfo.End_date != null)
                            {
                                if (discountInfo.Start_date <= today && discountInfo.End_date >= today)
                                {
                                    discountInfo.Status = "Đang diễn ra";
                                    await discountService.Update(discountInfo).ConfigureAwait(false);
                                }
                                else if (discountInfo.End_date < today)
                                {
                                    discountInfo.Status = "Đã kết thúc";
                                    await discountService.Update(discountInfo).ConfigureAwait(false);
                                }
                                else if (discountInfo.Start_date > today)
                                {
                                    discountInfo.Status = "Sắp diễn ra";
                                    await discountService.Update(discountInfo).ConfigureAwait(false);
                                }

                                var productIds = await productDiscountService.GetByIdDiscount(discountInfo.Id).ConfigureAwait(false);
                                var productTasks = productIds.Select(async productId =>
                                {
                                    var product = await productService.GetProductAttributesById(productId.ProductAttributes.Id).ConfigureAwait(false);
                                    if (product != null)
                                    {
                                        if (discountInfo.Status == "Đang diễn ra")
                                        {
                                            product.Sale_price = (long?)Math.Round(CalculateSalePrice(
                                                product.Regular_price ?? 0,
                                                discountInfo.Discount_value,
                                                discountInfo.Type_of_promotion
                                            ));
                                        }
                                        else
                                        {
                                            product.Sale_price = null; // Khôi phục giá gốc
                                        }
                                        await productService.Update(product, product.Id).ConfigureAwait(false);
                                    }
                                });

                                await Task.WhenAll(productTasks).ConfigureAwait(false);
                            }
                            else
                            {
                                discountInfo.Status = "Không xác định"; // Hoặc một trạng thái khác phù hợp
                            }
                        }
                    });

                    await Task.WhenAll(tasks).ConfigureAwait(false);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // kiểm tra mỗi 100 giây
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
                return regularPrice - discountValue;
            }
            return regularPrice; 
        }
    }
}