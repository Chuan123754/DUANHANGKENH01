using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class DiscountServices : IDiscountServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public DiscountServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<List<Discount>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/Discount";
            var response = await _httpClient.GetStringAsync(requestURL);
            var discount = JsonConvert.DeserializeObject<List<Discount>>(response);
            return discount;
        }

        public async Task<Discount> Details(long id)
        {
            // Lấy chi tiết Voucher theo ID từ API
            string requestURL = $"{_baseUrl}/api/Discount/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            var discount = JsonConvert.DeserializeObject<Discount>(response);
            return discount;
        }

        public async Task<Discount> Create(Discount discount)
        {
            // Tạo mới Voucher
            string requestURL = $"{_baseUrl}/api/Discount/post";
            var jsonContent = JsonConvert.SerializeObject(discount);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create voucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }

            // Deserialize JSON response để lấy đối tượng voucher mới được tạo
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdDiscount = JsonConvert.DeserializeObject<Discount>(responseContent);

            return createdDiscount;
        }


        public async Task<bool> Update(Discount discount)
        {
            // Cập nhật Voucher
            string requestURL = $"{_baseUrl}/api/Discount/put";
            var jsonContent = JsonConvert.SerializeObject(discount);
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
            // Xóa Voucher theo ID
            string requestURL = $"{_baseUrl}/api/Discount/{id}";
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
