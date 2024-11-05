using ClientViews.Models;
using Microsoft.AspNetCore.Identity;
using ClientViews.Models.DTO;

namespace ClientViews.IServices
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
        public Task SignOutAsync();
        public Task<IdentityResult> UpdateAccountAsync(Account account , string id);
        public Task<IdentityResult> DeleteAccountAsync(string idAccount);
        public Task<Account> GetAccountById(string idAccount);
        public Task<List<AccountWithRoles>> GetAllAccountsAsync();
        public Task<IdentityResult> ToggleLockAccountAsync(string idAccount);
    }
}
