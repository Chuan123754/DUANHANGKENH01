using Views.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IUserVoucherService
    {
        Task<List<UserVouchers>> GetAll();
        Task<UserVouchers> Details(long id);
        Task<bool> Create(UserVouchers userVoucher);
        Task<bool> Update(UserVouchers userVoucher);
        Task<bool> Delete(long id);
    }
}
