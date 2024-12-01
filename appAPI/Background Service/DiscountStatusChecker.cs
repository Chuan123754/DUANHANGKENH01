using appAPI.Models;
using appAPI.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace appAPI.Background_Service
{
    public class DiscountStatusChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

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
                    var variantsDiscountRepository = scope.ServiceProvider.GetRequiredService<IRepository<P_attribute_discount>>();

                    var discounts = discountRepository.GetAll();

                    foreach (var discount in discounts)
                    {
                        // Kiểm tra nếu Discount đã hết hạn và chuyển trạng thái thành "Không hoạt động"
                        if (discount.End_date <= DateTime.Now && discount.Status != "Đã kết thúc")
                        {
                            discount.Status = "Đã kết thúc";
                            discountRepository.Update(discount);

                            // Cập nhật trạng thái "Không hoạt động"
                            var relatedVariantsDiscounts = variantsDiscountRepository.GetAll().Where(p => p.Discount_Id == discount.Id);
                            foreach (var variantDiscount in relatedVariantsDiscounts)
                            {
                                if (variantDiscount.Status != "Đã kết thúc")
                                {
                                    variantDiscount.Status = "Đã kết thúc";
                                    variantsDiscountRepository.Update(variantDiscount);
                                }
                            }
                        }
                        // Kiểm tra nếu Discount là "Hoạt động"
                        else if (discount.Status == "Đang diễn ra")
                        {
                            var relatedVariantsDiscounts = variantsDiscountRepository.GetAll().Where(p => p.Discount_Id == discount.Id);
                            foreach (var variantDiscount in relatedVariantsDiscounts)
                            {
                                // Chuyển thành "Hoạt động" nếu chưa được cập nhật
                                if (variantDiscount.Status != "Đang diễn ra")
                                {
                                    variantDiscount.Status = "Đang diễn ra";
                                    variantsDiscountRepository.Update(variantDiscount);
                                }
                            }
                        }
                    }
                }
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
