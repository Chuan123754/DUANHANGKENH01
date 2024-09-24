using AppAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class WarehouseRepository : IWareHouseRepository
    {
        APP_DATA_DATN context;
        public WarehouseRepository()
        {
            context = new APP_DATA_DATN();
        }
        public async Task Create(Warehouse whs)
        {
            context.Warehouse.Add(whs);
            await context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var data = context.Warehouse.Find(id);
            context.Warehouse.Remove(data);
            await context.SaveChangesAsync();
        }

        public async Task<Warehouse> Details(long id)
        {
            return await context.Warehouse.FindAsync(id);
            
        }

        public async Task<List<Warehouse>> GetAll()
        {
            return await context.Warehouse.ToListAsync();
        }

        public async Task Update(Warehouse whs)
        {
            context.Entry(whs).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
