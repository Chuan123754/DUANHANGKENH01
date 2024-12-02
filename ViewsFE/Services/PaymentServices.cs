using appAPI.IRepository;
using appAPI.Models;
using Newtonsoft.Json;
using ViewsFE.IServices;

namespace ViewsFE.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly HttpClient _client;

        //public Task Create(Payment payment)
        //{
        //    throw new NotImplementedException();
        //}
        public PaymentServices(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Payment>> GetAll()
        {
            string requestURL = "https://localhost:7011/api/TypePayment/GetAll";
            var response = await _client.GetStringAsync(requestURL);
            return  JsonConvert.DeserializeObject<List<Payment>>(response);
        }
    }
}
