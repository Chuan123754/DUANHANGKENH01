using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface ISizeServices
    {
        Task<List<Size>> GetAll();
        Task<Size> Details(long id);
        Task Create(Size s);
        Task Update(Size s);
        Task Delete(long id);
        Task<List<Size>> Search(string keyword);
    }
}
