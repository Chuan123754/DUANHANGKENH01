using ClientViews.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClientViews.IServices
{
    public interface IUserVoucherService
    {
        Task<List<UserVouchers>> GetAll();
        Task<UserVouchers> Details(long id);
        Task<List<UserVouchers>> GetByVoucherId(long voucherId);
        Task<UserVouchers> GetByVoucherIdAndUserId(long voucherId, long userId);
        Task<bool> Create(UserVouchers userVoucher);
        Task<bool> Update(UserVouchers userVoucher);
        Task<bool> Delete(long id);
    }
}
