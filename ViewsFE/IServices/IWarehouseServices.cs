using ViewsFE.Models;

namespace ViewsFE.IServices
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
