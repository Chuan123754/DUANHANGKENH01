using appViews.Models;
using Microsoft.AspNetCore.Identity;

namespace AppViews.IServices
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
