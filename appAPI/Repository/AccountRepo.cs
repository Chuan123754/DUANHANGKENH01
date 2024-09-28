using AppAPI.IRepository;
using appAPI.Helper;
using appAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppAPI.Repository
{
    public class AccountRepo: IAccountRepo
    {
        private readonly UserManager<Account> userManager;
        private readonly SignInManager<Account> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepo(UserManager<Account> userManager , SignInManager<Account> signInManager , IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Email , model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(10), 
                claims: authClaim,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512) 
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            try
            {
                var account = new Account
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };
                var result =  await userManager.CreateAsync(account , model.Password);
                if (result.Succeeded)
                {
                    var validRoles = new List<string>
                    {
                        ApplicationRole.Admin,
                        ApplicationRole.Employee,
                        ApplicationRole.Designer,
                        ApplicationRole.SuperAdmin,
                        ApplicationRole.Customer
                    };
                    if (!validRoles.Contains(model.Role))
                    {
                        throw new InvalidOperationException("Vai trò lựa chọn không hợp lệ");
                    }
                    if (!await roleManager.RoleExistsAsync(model.Role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(model.Role));
                    }
                    await userManager.AddToRoleAsync(account, model.Role);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Xảy ra lỗi khi tạo tài khoản: {ex.Message}");
                throw;
            }
        }
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
