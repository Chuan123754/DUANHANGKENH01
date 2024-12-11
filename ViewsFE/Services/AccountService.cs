using ViewsFE.IServices;
using ViewsFE.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;
using ViewsFE.Models.DTO;
using System.Net.Http.Headers;
using Blazored.SessionStorage;
using System.Text.Json;

namespace ViewsFE.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly ISessionStorageService _sessionStorage;

        public AccountService(HttpClient client , ISessionStorageService sessionStorage)
        {
            _client = client;
            _sessionStorage= sessionStorage;
        }
        public async Task<List<Account>> GetAll() {
            string requestURL = "https://localhost:7011/api/Account/GetAll";
            var request =  await _client.GetStringAsync(requestURL);
            var item = JsonConvert.DeserializeObject<List<Account>>(request);
            return item;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            string requestURL = "https://localhost:7011/api/Account/Login";
            var response = await _client.PostAsJsonAsync(requestURL, model);

            // Đọc nội dung phản hồi  
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {content}"); // Ghi lại nội dung phản hồi để kiểm tra  

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Xóa ký tự dấu ngoặc kép bên ngoài nếu chúng có  
                    if (content.StartsWith("\"") && content.EndsWith("\""))
                    {
                        content = content[1..^1]; // Loại bỏ ký tự đầu và cuối  
                    }

                    // Phân tích nội dung JSON  
                    var result = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(content); // Sử dụng System.Text.Json  

                    if (result != null && !string.IsNullOrEmpty(result.token))
                    {
                        return result.token;
                    }
                    else
                    {
                        // Nếu không có token  
                        throw new Exception("Token không hợp lệ hoặc không tìm thấy.");
                    }
                }
                catch (System.Text.Json.JsonException jsonEx)
                {
                    // Xử lý ngoại lệ phân tích JSON  
                    Console.WriteLine($"Lỗi phân tích JSON: {jsonEx.Message}\nNội dung phản hồi: {content}");
                    throw new Exception("Lỗi phân tích phần phản hồi của API.");
                }
            }

            throw new Exception("Đăng nhập không hợp lệ.");
        }

        public class TokenResponse
        {
            public string token { get; set; }
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            string requestURL = "https://localhost:7011/api/Account/SignUp";
            var response = await _client.PostAsJsonAsync(requestURL, model);
            if (response.IsSuccessStatusCode)
            {
                return IdentityResult.Success;
            }
            var errors = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return IdentityResult.Failed(errors.Select(error => new IdentityError { Description = error }).ToArray());
        }
        public async Task SignOutAsync()
        {
            string requestURL = "https://localhost:7011/api/Account/SignOut";
            var response = await _client.PostAsync(requestURL,null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Đăng xuất không thành công");
            }
        }
        public async Task<IdentityResult> UpdateAccountAsync(Account account, string id)
        {
            string requestURL = $"https://localhost:7011/api/Account/UpdateAccount/{id}";
            var jsonContent = JsonConvert.SerializeObject(account);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(requestURL, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IdentityResult>(responseContent);
            }
            else
            {
                throw new Exception("Unable to update account.");
            }

        }
        public async Task<IdentityResult> DeleteAccountAsync(string idAccount)
        {
            string requestURL = $"https://localhost:7011/api/Account/DeleteAccount?idAccount={idAccount}";
            var response =  await _client.DeleteAsync(requestURL);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IdentityResult>();
                return result;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Unable to delete account: {errorMessage}");
            }
        }
        public async Task<Account> GetAccountById(string idAccount)
        {
            string requestURL = $"https://localhost:7011/api/Account/GetById?idAccount={idAccount}";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Account>(response);
        }
        public async Task<List<AccountWithRoles>> GetAllAccountsAsync()
        {
            string requestURL = $"https://localhost:7011/api/Account/GetAllAccount";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<AccountWithRoles>>(response);
        }
        public async Task<IdentityResult> ToggleLockAccountAsync(string idAccount)
        {
            string requestURL = $"https://localhost:7011/api/Account/ToggleLock/{idAccount}";
            var response = await _client.PatchAsync(requestURL, null);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IdentityResult>();
                return result;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Unable to toggle lock account: {errorMessage}");
            }
        }

        public async Task<string> GetPasswordHashByEmail(string email)
        {
            string requestURL = $"https://localhost:7011/api/Account/GetPasswordHashByEmail?email={email}";
            var response = await _client.GetAsync(requestURL);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Không thể lấy PasswordHash.");
            }
        }

    }
}
