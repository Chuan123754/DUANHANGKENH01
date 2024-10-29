using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IDiscountServices
    {
        Task<List<Discount>> GetAll();
        Task<Discount> Details(long id);
        Task<Discount> Create(Discount discount);
        Task<bool> Update(Discount discount);
        Task<bool> Delete(long id);
    }
}
