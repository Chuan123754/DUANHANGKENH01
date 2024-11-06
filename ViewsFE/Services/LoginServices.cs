using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using appAPI.Models.DTO;
using Newtonsoft.Json;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace appAPI.Services
{
    public class LoginServices : IloginServices
    {
        private readonly HttpClient _httpClient;

        public LoginServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Phương thức SignUp
        public async Task<string> SignUp(SignUpModel model)
        {
            string requestURL = "https://localhost:7011/api/Account/SignUp";
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);

            if (response.IsSuccessStatusCode)
            {
                return "Đăng ký thành công!";
            }
            else
            {
                return "Đăng ký thất bại: " + response.ReasonPhrase;
            }
        }

        // Phương thức Login
        public async Task<string> Login(SignInModel model)
        {
            string requestURL = "https://localhost:7011/api/Account/Login";
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);

            if (response.IsSuccessStatusCode)
            {
                // Đọc token từ body response
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
            else
            {
                return "Đăng nhập thất bại: " + response.ReasonPhrase;
            }
        }

        // Phương thức SignOut
        public async Task<bool> SignOut()
        {
            string requestURL = "https://localhost:7011/api/Account/SignOut";
            var response = await _httpClient.PostAsync(requestURL, null);

            return response.IsSuccessStatusCode;
        }
    }
}
