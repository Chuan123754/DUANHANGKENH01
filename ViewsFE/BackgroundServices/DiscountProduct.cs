using ViewsFE.IServices;

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
                    // Lấy các dịch vụ scoped từ scope  
                    var discountService = scope.ServiceProvider.GetRequiredService<IDiscountServices>();
                    var productService = scope.ServiceProvider.GetRequiredService<IProductAttributeServices>();
                    var productDiscountService = scope.ServiceProvider.GetRequiredService<IAttributesDiscountServices>();

                    var discounts = await productDiscountService.GetAll();
                    var today = DateTime.Now;

                    foreach (var discount in discounts)
                    {
                        if (discount != null)
                        {
                            if (discount.Discount != null)
                            {
                                if (discount.Discount.Start_date != null && discount.Discount.End_date != null)
                                {

                                    // So sánh ngày  
                                    if (discount.Discount.Start_date <= today && discount.Discount.End_date >= today)
                                    {
                                        discount.Discount.Status = "Đang diễn ra";

                                        await discountService.Update(discount.Discount);

                                        var productIds = await productDiscountService.GetByIdDiscount(discount.Discount.Id);
                                        foreach (var productId in productIds)
                                        {
                                            var product = await productService.GetProductAttributesById(productId.ProductAttributes.Id);
                                            product.Sale_price = (long?)Math.Round(CalculateSalePrice(
                                                product.Regular_price ?? 0,
                                                discount.Discount.Discount_value,
                                                discount.Discount.Type_of_promotion
                                            ));
                                            await productService.Update(product, product.Id);
                                        }
                                    }
                                    else if (discount.Discount.End_date < today)
                                    {
                                        discount.Discount.Status = "Đã kết thúc";
                                        await discountService.Update(discount.Discount);

                                        var productIds = await productDiscountService.GetByIdDiscount(discount.Discount.Id);
                                        foreach (var productId in productIds)
                                        {
                                            var product = await productService.GetProductAttributesById(productId.ProductAttributes.Id);
                                            if (product.Regular_price.HasValue)
                                            {
                                                product.Sale_price = null;  // Khôi phục giá gốc  
                                                await productService.Update(product, product.Id);
                                            }
                                        }
                                    }
                                    else if (discount.Discount.Start_date > today)
                                    {
                                        discount.Discount.Status = "Sắp diễn ra";
                                        await discountService.Update(discount.Discount);
                                        var productIds = await productDiscountService.GetByIdDiscount(discount.Discount.Id);
                                        foreach (var productId in productIds)
                                        {
                                            var product = await productService.GetProductAttributesById(productId.ProductAttributes.Id);
                                            if (product.Regular_price.HasValue)
                                            {
                                                product.Sale_price = null;  // Khôi phục giá gốc  
                                                await productService.Update(product, product.Id);
                                            }
                                        }
                                    }
                                }
                                else
                                { 
                                    discount.Discount.Status = "Không xác định"; 
                                }
                            }
                            else
                            {
                                discount.Discount.Status = "Không xác định";  
                            }
                        }
                        else
                        {
                            continue; 
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);  
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