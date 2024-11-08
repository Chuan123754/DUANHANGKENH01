using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class OrderServices : OrderIServices
    {
        private readonly HttpClient _client;

        public OrderServices(HttpClient client)
        {
            _client = client;
        }
        public async Task Create(Orders order)
        {
            await _client.PostAsJsonAsync("https://localhost:7011/api/Orders/Create", order);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"https://localhost:7011/api/Orders/Delete?id={id}");
        }

        public async Task<List<Orders>> GetAll()
        {
            string requestURL = "https://localhost:7011/api/Orders/All-Orders";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Orders>>(response);
        }

        public Task<Orders> GetByIdOrders(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Orders>> Search(string code)
        {
            throw new NotImplementedException();
        }

        public Task Update(Orders orders)
        {
            throw new NotImplementedException();
        }
    }
}
