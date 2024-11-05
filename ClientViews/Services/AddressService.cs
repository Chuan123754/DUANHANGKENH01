using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClientViews.Models;
using Newtonsoft.Json;
using ClientViews.IServices;

namespace ClientViews.Services
{
    public class AddressService: IAddressServices
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Address>> GetAll()
        {
            string requestURL = "https://localhost:7011/api/Address/GetAllAddress";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Address>>(response);
        }
        public async Task<List<Address>> GetAddressByUserId(long userId)
        {
            string requestURL = $"https://localhost:7011/api/Address/GetAddressByUserId?userId={userId}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Address>>(response);
        }
        public async Task<Address> GetAddressById(long id)
        {
            string requestURL = $"https://localhost:7011/api/Address/GetAddressById?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Address>(response);
        }
        public async Task CreateAddress(Address address)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7011/api/Address/CreateAddress", address);
        }
        public async Task UpdateAddress( Address address, long id)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7011/api/Address/UpdateAddress?id={id}", address);
        }
        public async Task SetAsDefault(long id, Address address)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7011/api/Address/SetAsDefault?id={id}",address);
        }
        public async Task DeleteAddress(long id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7011/api/Address/DeleteAddress?id={id}");
        }
        // lấy api địa chỉ Việt Nam từ bên tứ 3
        public async Task<List<Province>> GetProvincesAsync()
        {
            string url = "https://open.oapi.vn/location/provinces?size=100"; 
            var response = await _httpClient.GetFromJsonAsync<ProvinceResponse>(url);

            return response?.Data ?? new List<Province>();
        }
        public async Task<List<Districted>> GetDistrictsAsync(int idProvince)
        {
            string url = $"https://open.oapi.vn/location/districts/{idProvince}?size=100";
            var response = await _httpClient.GetFromJsonAsync<DistrictResponse>(url);

            return response?.Data ?? new List<Districted>();
        }

        public async Task<List<Ward>> GetWardsAsync(int idDistrict)
        {
            string url = $"https://open.oapi.vn/location/wards/{idDistrict}";
            var response = await _httpClient.GetFromJsonAsync<WardResponse>(url); 

            return response?.Data ?? new List<Ward>();
        }

    }
    // Định nghĩa cấu trúc dữ liệu phản hồi từ API
    public class ProvinceResponse
    {
        public List<Province> Data { get; set; } 
        public string Code { get; set; }
    }
    public class DistrictResponse
    {
        public List<Districted> Data { get; set; }
        public string Code { get; set; }
    }

    public class WardResponse
    {
        public List<Ward> Data { get; set; }
        public string Code { get; set; }
    }

    public class Province
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeText { get; set; }
        public string Slug { get; set; }
    }
    public class Districted
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProvinceId { get; set; } // Để tham chiếu đến tỉnh
        public int Type { get; set; }
        public string TypeText { get; set; }
    }

    public class Ward
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DistrictId { get; set; } // Để tham chiếu đến huyện
        public int Type { get; set; }
        public string TypeText { get; set; }
    }

}
