using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IWareHouseRepository
    {
        Task<List<Warehouse>> GetAll();
        Task<Warehouse> Details(long id);
        Task Create(Warehouse whs);
        Task Update(Warehouse whs);
        Task Delete(long id);
    }
}
