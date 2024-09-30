using ViewsFE.IServices;
using ViewsFE.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;

namespace ViewsFE.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            string requestURL = "https://localhost:7011/api/Account/Login";
            var response = await _client.PostAsJsonAsync(requestURL, model);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new Exception("Đăng nhập không hợp lệ");
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
                return await response.Content.ReadFromJsonAsync<IdentityResult>();
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
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            string requestURL = $"https://localhost:7011/api/Account/GetAllAccount";
            var response = await _client.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Account>>(response);
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
    }
}
