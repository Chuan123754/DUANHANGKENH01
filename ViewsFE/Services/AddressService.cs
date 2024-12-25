using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ViewsFE.Models;
using Newtonsoft.Json;
using ViewsFE.IServices;
using static ViewsFE.Services.PostServices;
using System.Text;

namespace ViewsFE.Services
{
    public class AddressService: IAddressServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private const string ApiToken123 = "3ddaa952-b6c6-11ef-96fc-be30eacd7c1b";
        public AddressService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task<List<Address>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/Address/GetAllAddress";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Address>>(response);
        }
        public async Task<List<Address>> GetAddressByUserId(long userId)
        {

            string requestURL = $"{_baseUrl}/api/Address/GetAddressByUserId?userId={userId}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Address>>(response);
        }
        public async Task<Address> GetAddressById(long id)
        {
            string requestURL = $"{_baseUrl}/api/Address/GetAddressById?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Address>(response);
        }
        public async Task<bool> CreateAddress(Address address)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Address/CreateAddress", address);

                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}"); 
                    return false;  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false; 
            }

        }

        public async Task<Address> CreateAddressAndReturn(Address address)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Address/CreateAddressAndReturn", address);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Address>(); // Trả về đối tượng Address vừa được tạo
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                    return null; // Trả về null nếu có lỗi
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }


        public async Task UpdateAddress( Address address, long id)
        {
            await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Address/UpdateAddress?id={id}", address);
        }
        public async Task SetAsDefault(long id, Address address)
        {
            await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Address/SetAsDefault?id={id}",address);
        }
        public async Task DeleteAddress(long id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/api/Address/DeleteAddress?id={id}");
        }
        // lấy api địa chỉ Việt Nam từ bên tứ 3
        public async Task<List<Provinces>> GetProvincesAsync()
        {
            string apiUrl = "https://online-gateway.ghn.vn/shiip/public-api/master-data/province";
            string token = "3ddaa952-b6c6-11ef-96fc-be30eacd7c1b"; // Đảm bảo Token đúng

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, apiUrl))
                {
                    request.Headers.Add("Token", token);

                    // Gửi Request
                    var response = await _httpClient.SendAsync(request);

                    // Kiểm tra Response
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OKAPI>(jsonResponse);
                        return apiResponse?.Data ?? new List<Provinces>();
                    }
                    else
                    {
                        throw new Exception($"API call failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Provinces>();
            }
        }


        public async Task<List<Districts>> GetDistrictsByProvinceAsync(int provinceId)
        {
            string apiUrl = "https://online-gateway.ghn.vn/shiip/public-api/master-data/district";
            string token = "3ddaa952-b6c6-11ef-96fc-be30eacd7c1b"; // Replace with your actual token

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)) 
                {
                    request.Headers.Add("Token", token);

                    var requestBody = new { province_id = provinceId };
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                    var response = await _httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OKAPI_District>(jsonResponse); // Modify according to your response model
                        return apiResponse?.Data ?? new List<Districts>();
                    }
                    else
                    {
                        throw new Exception($"API call failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Districts>();
            }
        }


        public async Task<List<Wards>> GetWardsByDistrictAsync(int districtId)
        {
            string apiUrl = $"https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id={districtId}";
            string token = "3ddaa952-b6c6-11ef-96fc-be30eacd7c1b"; // Đảm bảo Token đúng

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, apiUrl))
                {
                    // Thêm Header Token
                    request.Headers.Add("Token", token);

                    // Gửi Request
                    var response = await _httpClient.SendAsync(request);

                    // Kiểm tra Response
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<OKAPI_Ward>(jsonResponse);
                        return apiResponse?.Data ?? new List<Wards>();
                    }
                    else
                    {
                        throw new Exception($"API call failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Wards>();
            }
        }
        public async Task<decimal> CalculateShippingFee(ShippingRequest request)
        {
            var url = "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee";
            string token = "01ce57f1-bdf0-11ef-a5c1-de81edc738f2";
            int shopId = 5534739;

            decimal feeShipping = 0;

            try
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    httpRequest.Headers.Add("Token", token);
                    httpRequest.Headers.Add("ShopId", shopId.ToString());

                    var jsonContent = JsonConvert.SerializeObject(request);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    httpRequest.Content = content;

                    var response = await _httpClient.SendAsync(httpRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Phí dịch vụ: " + responseContent);

                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        feeShipping = Convert.ToDecimal(jsonResponse.data.total); // Chuyển đổi thành decimal  

                        Console.WriteLine($"Phí shipping: {feeShipping}");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Lỗi khi gọi API: {response.ReasonPhrase} - Content: {errorContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return feeShipping;
        }

    }

    public class OKAPI
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Provinces> Data { get; set; }
    }

    public class OKAPI_District
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Districts> Data { get; set; }
    }

    public class OKAPI_Ward
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Wards> Data { get; set; }
    }

    public class Provinces
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string Code { get; set; }
    }

    public class Districts
    {
        public int DistrictID { get; set; }
        public int ProvinceID { get; set; }
        public string DistrictName { get; set; }
        public string Code { get; set; }
        public int Type { get; set; }
        public int SupportType { get; set; }
    }

    public class Wards
    {
        public string WardCode { get; set; }
        public int DistrictID { get; set; }
        public string WardName { get; set; }
    }

    public class ShippingRequest
    {
        public int? service_type_id { get; set; }
        public int? insurance_value { get; set; }
        public string? coupon { get; set; }
        public int? cod_failed_amount { get; set; }
        public int? from_district_id { get; set; }
        public string? from_ward_code { get; set; }
        public int? to_district_id { get; set; }
        public string? to_ward_code { get; set; }
        public int? weight { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }




        public int? cod_value { get; set; }
        public Item[]? items { get; set; }
    }

    public class Item
    {
        public string? name { get; set; }
        public string? code { get; set; }
        public int? quantity { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? weight { get; set; }
    }

    public class ResponseAPI
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Response Data { get; set; }
    }
    public class Response
    {

    }
}
