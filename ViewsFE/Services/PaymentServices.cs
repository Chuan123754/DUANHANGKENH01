//using appAPI.IRepository;
//using appAPI.Models;
//using Newtonsoft.Json;
//using ViewsFE.IServices;

//namespace ViewsFE.Services
//{
//    public class PaymentServices : IPaymentServices
//    {
//        private readonly HttpClient _client;
//        private readonly string _baseUrl;

//        //public Task Create(Payment payment)
//        //{
//        //    throw new NotImplementedException();
//        //}
//        public PaymentServices(HttpClient client, IConfiguration configuration)
//        {
//            _client = client;
//            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
//        }
//        public async Task<List<Payment>> GetAll()
//        {
//            string requestURL = $"{_baseUrl}/api/TypePayment/GetAll";
//            var response = await _client.GetStringAsync(requestURL);
//            return  JsonConvert.DeserializeObject<List<Payment>>(response);
//        }
//    }
//}
