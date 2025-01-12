using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Background_Service
{
    public class VoucherExpiryChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);
        public VoucherExpiryChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var voucherRepository = scope.ServiceProvider.GetRequiredService<IRepository<Vouchers>>();
                    var vouchers = voucherRepository.GetAll();

                    foreach (var voucher in vouchers)
                    {
                        if (voucher.End_time <= DateTime.Now && voucher.Status != "Đã kết thúc")
                        {
                            voucher.Status = "Đã kết thúc";
                        }
                        else if (voucher.Start_time > DateTime.Now && voucher.Status != "Sắp diễn ra")
                        {
                            voucher.Status = "Sắp diễn ra";
                        }
                        else if(DateTime.Now >= voucher.Start_time && DateTime.Now < voucher.End_time && voucher.Status != "Đang diễn ra")
                        {
                            voucher.Status = "Đang diễn ra";
                        }
                        voucherRepository.Update(voucher);
                        //Kiểm tra số lượng.
                        if (voucher.Quantity == "0" && voucher.Status != "Hết hàng")
                        {
                            voucher.Status = "Hết hàng";
                          
                        }
                    }
                }

                // Đợi trước khi thực hiện lần kiểm tra tiếp theo
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
