using appAPI.Models;
using appAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appAPI.Background_Service
{
    public class VoucherExpiryChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(30);
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
                        if (voucher.End_time <= DateTime.Now && voucher.Status != "Hết hạn")
                        {
                            voucher.Status = "Hết hạn";
                            voucherRepository.Update(voucher);
                        }

                        if (voucher.Start_time > DateTime.Now && voucher.Status != "Chưa kích hoạt")
                        {
                            voucher.Status = "Chưa kích hoạt";
                            voucherRepository.Update(voucher);
                        }

                        if (voucher.Quantity == "0" && voucher.Status != "Hết hàng")
                        {
                            voucher.Status = "Hết hàng";
                            voucherRepository.Update(voucher);
                        }                        
                    }
                }

                // Đợi trước khi thực hiện lần kiểm tra tiếp theo
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
