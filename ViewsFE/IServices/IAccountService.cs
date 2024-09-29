using Views.Models;
using Microsoft.AspNetCore.Identity;

namespace AppViews.IServices
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
        public Task SignOutAsync();
        public Task<IdentityResult> UpdateAccountAsync(Account account , string id);
        public Task<IdentityResult> DeleteAccountAsync(string idAccount);
        public Task<Account> GetAccountById(string idAccount);
        public Task<List<Account>> GetAllAccountsAsync();
        public Task<IdentityResult> ToggleLockAccountAsync(string idAccount);
    }
}
