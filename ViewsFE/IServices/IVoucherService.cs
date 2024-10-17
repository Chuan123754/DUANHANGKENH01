using Views.Models;

namespace ViewsFE.IServices
{
    public interface IVoucherService
    {
        Task<List<Vouchers>> GetAll();
        Task<List<Users>> GetUsers();
        Task<Vouchers> Details(long id);
        Task<Vouchers> Create(Vouchers vouchers);
        Task<bool> Update(Vouchers vouchers);
        Task<bool> Delete(long id);
    }
}
