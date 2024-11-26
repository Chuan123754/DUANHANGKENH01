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

                    // Lấy tất cả các Discount
                    var discounts = discountRepository.GetAll();

                    foreach (var discount in discounts)
                    {
                        // Kiểm tra nếu Discount đã hết hạn và chuyển trạng thái thành "Không hoạt động"
                        if (discount.End_date <= DateTime.Now && discount.Status != "Không hoạt động")
                        {
                            discount.Status = "Không hoạt động";
                            discountRepository.Update(discount);

                            // Cập nhật trạng thái "Không hoạt động" cho các bản ghi P_attribute_discount liên quan
                            var relatedVariantsDiscounts = variantsDiscountRepository.GetAll().Where(p => p.Discount_Id == discount.Id);
                            foreach (var variantDiscount in relatedVariantsDiscounts)
                            {
                                if (variantDiscount.Status != "Không hoạt động")
                                {
                                    variantDiscount.Status = "Không hoạt động";
                                    variantsDiscountRepository.Update(variantDiscount);
                                }
                            }
                        }
                        // Kiểm tra nếu Discount là "Hoạt động" và chưa cập nhật trạng thái của các P_attribute_discount liên quan
                        else if (discount.Status == "Hoạt động")
                        {
                            var relatedVariantsDiscounts = variantsDiscountRepository.GetAll().Where(p => p.Discount_Id == discount.Id);
                            foreach (var variantDiscount in relatedVariantsDiscounts)
                            {
                                // Chuyển các P_attribute_discount liên quan thành "Hoạt động" nếu chưa được cập nhật
                                if (variantDiscount.Status != "Hoạt động")
                                {
                                    variantDiscount.Status = "Hoạt động";
                                    variantsDiscountRepository.Update(variantDiscount);
                                }
                            }
                        }
                    }
                }

                // Đợi trước khi thực hiện lần kiểm tra tiếp theo
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
