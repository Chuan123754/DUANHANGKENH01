using Views.Models;

namespace Views.IServices
{
    public interface IWarehouseServices
    {
        Task<List<Warehouse>> GetAll();
        Task<Warehouse> Details(long id);
        Task Create(Warehouse whs);
        Task Update(Warehouse whs);
        Task Delete(long id);
    }
}
