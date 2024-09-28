 using AppViews.IServices;
using Views.Models;
using Microsoft.AspNetCore.Identity;

namespace Views.Services
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
    }
}
