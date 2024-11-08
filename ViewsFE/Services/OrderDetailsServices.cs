using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class OrderDetailsServices : OrderDetailsIServices
    {
        private readonly HttpClient _client;

        public OrderDetailsServices(HttpClient client)
        {
            _client = client;
        }
        public async Task Create(Order_details orderdetails)
        {
            await _client.PostAsJsonAsync("https://localhost:7011/api/OrderDetails/Create", orderdetails);
        }

        public async Task Delete(long id)
        {
            await _client.DeleteAsync($"https://localhost:7011/api/OrderDetails/Delete?id={id}");
        }

        public async Task<List<Order_details>> GetAlldetail()
        {
            string requestURL = "https://localhost:7011/api/OrderDetails/All";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Order_details>>(response);
        }

        public async Task<Order_details> GetByIdOrderdetails(long id)
        {
            string requestURL = $"https://localhost:7011/api/OrderDetails/Details?id={id}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Order_details>(response);
        }

        public async Task<List<Order_details>> GetOrderDetailsByOrderId(long idOrder)
        {
            string requestURL = $"https://localhost:7011/api/OrderDetails/GetOrderDetailsByOrderId?idOrder={idOrder}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Order_details>>(response);
        }

        public async Task Update(Order_details orderdetails , long id)
        {
            await _client.PutAsJsonAsync($"https://localhost:7011/api/OrderDetails/Update?id={id}", orderdetails);
        }
    }
}
