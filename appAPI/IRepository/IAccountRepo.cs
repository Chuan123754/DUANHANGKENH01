using appAPI.Models;
using appAPI.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace appAPI.IRepository
{
    public interface IAccountRepo
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model); // trả về True / False hoặc chuỗi Token
        public Task SignOutAsync();
        public Task<IdentityResult> UpdateAccountAsync(Account account , string id);
        public Task<IdentityResult> DeleteAccountAsync(string idAccount);
        public Task<Account> GetAccountById(string idAccount);
        public Task<List<AccountWithRoles>> GetAllAccountsAsync();
        public Task<IdentityResult> ToggleLockAccountAsync(string idAccount);
    }
}
