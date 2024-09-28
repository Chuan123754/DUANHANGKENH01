using ViewsFE.Models;
using Microsoft.AspNetCore.Identity;

namespace ViewsFE.IServices
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
