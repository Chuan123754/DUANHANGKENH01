using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IVoucherRepository
    {
        Task<List<UserVouchers>> GetAll(); 
    }
}
