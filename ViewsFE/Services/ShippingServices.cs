//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace ViewsFE.Services
//{
//    public class ShippingServices
//    {
//        private readonly HttpClient _httpClient;

//        public ShippingServices(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<List<Provinces>> GetProvinces()
//        {
//            try
//            {
//                string apiUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/province";
//                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

//                if (response.IsSuccessStatusCode)
//                {
//                    string jsonResponse = await response.Content.ReadAsStringAsync();
//                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
//                    return apiResponse?.Data ?? new List<Provinces>();
//                }
//                else
//                {
//                    throw new Exception($"API call failed with status code: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//                return new List<Provinces>();
//            }
//        }
//    }

//    public class ApiResponse
//    {
//        public int Code { get; set; }
//        public string Message { get; set; }
//        public List<Provinces> Data { get; set; }
//    }

//    public class Provinces
//    {
//        public int ProvinceID { get; set; }
//        public string ProvinceName { get; set; }
//        public string Code { get; set; }
//    }
//}
