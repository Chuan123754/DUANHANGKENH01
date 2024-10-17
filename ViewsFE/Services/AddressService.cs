using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ViewsFE.Services
{
    public class AddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Province>> GetProvincesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Province>>("https://provinces.open-api.vn/api/");
            return response; // Trả về danh sách tỉnh
        }

        public async Task<List<District>> GetDistrictsAsync(int code)
        {
            var response = await _httpClient.GetFromJsonAsync<Province>($"https://provinces.open-api.vn/api/p/{code}?depth=2");
            return response?.Districts ?? new List<District>(); // Trả về danh sách huyện dựa trên mã tỉnh
        }

        public async Task<List<Ward>> GetWardsAsync(int code)
        {
            var response = await _httpClient.GetFromJsonAsync<District>($"https://provinces.open-api.vn/api/d/{code}?depth=2");
            return response?.Wards ?? new List<Ward>(); // Trả về danh sách xã dựa trên mã huyện
        }
    }


    public class Province
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string DivisionType { get; set; }
        public int PhoneCode { get; set; }
        public string Codename { get; set; }
        public List<District> Districts { get; set; } // Danh sách huyện
    }

    public class District
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Codename { get; set; }
        public string DivisionType { get; set; }
        public int ProvinceCode { get; set; }
        public List<Ward> Wards { get; set; } // Danh sách xã (có thể là null)
    }

    public class Ward
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Codename { get; set; }
        public string DivisionType { get; set; }
        public int DistrictCode { get; set; }
    }

}
