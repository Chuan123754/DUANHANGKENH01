using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewsFE.Models;
using ViewsFE.IServices;

namespace ViewsFE.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public VoucherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<List<Vouchers>> GetAll()
        {
            // Lấy danh sách Voucher từ API
            string requestURL = $"{_baseUrl}/api/Vouchers";
            var response = await _httpClient.GetStringAsync(requestURL);
            var vouchers = JsonConvert.DeserializeObject<List<Vouchers>>(response);
            return vouchers;
        }

        public async Task<List<Users>> GetUsers()
        {
            string requestURL = $"{_baseUrl}/api/Users/Users-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Users>>(response);
        }

        public async Task<Vouchers> Details(long id)
        {
            string requestURL = $"{_baseUrl}/api/Vouchers/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            var voucher = JsonConvert.DeserializeObject<Vouchers>(response);
            return voucher;
        }

        public async Task<Vouchers> Create(Vouchers voucher)
        {
            string requestURL = $"{_baseUrl}/api/Vouchers/post";
            var jsonContent = JsonConvert.SerializeObject(voucher);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create voucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }

            // lấy đối tượng voucher mới được tạo
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdVoucher = JsonConvert.DeserializeObject<Vouchers>(responseContent);

            return createdVoucher;
        }


        public async Task<bool> Update(Vouchers voucher)
        {
            string requestURL = $"{_baseUrl}/api/Vouchers/put";
            var jsonContent = JsonConvert.SerializeObject(voucher);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to update voucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(long id)
        {
            string requestURL = $"{_baseUrl}/api/Vouchers/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete voucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
    }
}