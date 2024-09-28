using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class WarehouseServices : IWarehouseServices
    {
        HttpClient client;
        public WarehouseServices()
        {
            client = new HttpClient();
        }
        public async Task Create(Warehouse whs)
        {
            await client.PostAsJsonAsync("https://localhost:7015/api/Warehouse/add-warehouse", whs);
        }

        public async Task Delete(long id)
        {
            await client.DeleteAsync($"https://localhost:7015/api/Warehouse/delete-warehouse?id={id}");
        }

        public async Task<Warehouse> Details(long id)
        {
            return await client.GetFromJsonAsync<Warehouse>($"https://localhost:7015/api/Warehouse/details?id={id}");
        }

        public async Task<List<Warehouse>> GetAll()
        {
            return await client.GetFromJsonAsync<List<Warehouse>>("https://localhost:7015/api/Warehouse/show");
        }

        public async Task Update(Warehouse whs)
        {
            await client.PutAsJsonAsync("https://localhost:7015/api/Warehouse/edit-warehouse", whs);
        }
    }
}
