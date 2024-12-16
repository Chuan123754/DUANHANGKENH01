using appAPI.IRepository;
using appAPI.Helper;
using appAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using appAPI.Models.DTO;

namespace appAPI.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<Account> userManager;
        private readonly SignInManager<Account> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly APP_DATA_DATN context;

        public AccountRepo(UserManager<Account> userManager, SignInManager<Account> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager , APP_DATA_DATN context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        //public async Task<UsersWithRoles> SignInAsync(SignInModel model)
        //{
        //    // Tìm người dùng theo email
        //    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        //    if (user == null)
        //    {
        //        throw new Exception("Người dùng không tồn tại.");
        //    }

        //    // Kiểm tra mật khẩu
        //    var passwordHasher = new PasswordHasher<object>();
        //    var verificationResult = passwordHasher.VerifyHashedPassword(null, user.Password, model.Password);
        //    if (verificationResult != PasswordVerificationResult.Success)
        //    {
        //        throw new Exception("Mật khẩu không hợp lệ.");
        //    }

        //    // Lấy danh sách vai trò (roles)
        //    var account = await userManager.FindByEmailAsync(model.Email);
        //    if (account == null)
        //    {
        //        throw new Exception("Không thể tìm thấy tài khoản tương ứng.");
        //    }

        //    var roles = await userManager.GetRolesAsync(account);

        //    // Nếu xác thực thành công, trả về đối tượng Users cùng vai trò
        //    return new UsersWithRoles
        //    {
        //        User = user,
        //        Roles = roles.ToList()
        //    };
        //}



        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow) || !passwordValid)
            {
                return string.Empty;
            }

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Email , model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
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
                var result = await userManager.CreateAsync(account, model.Password);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Xảy ra lỗi khi tạo tài khoản: {ex.Message}");
                throw;
            }
        }
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> UpdateAccountAsync(Account account , string id)
        {
            try
            {
                var exitingAccount = await userManager.FindByIdAsync(id);
                if (exitingAccount == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Not Found" });
                }
                exitingAccount.FirstName = account.FirstName;
                exitingAccount.LastName = account.LastName;
                exitingAccount.UserName = account.UserName;
                exitingAccount.PhoneNumber = account.PhoneNumber;
                exitingAccount.Email = account.Email;
                var result = await userManager.UpdateAsync(exitingAccount);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<IdentityResult> DeleteAccountAsync(string idAccount)
        {
            try
            {
                var deleteItem = await userManager.FindByIdAsync(idAccount);
                if (deleteItem == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Not Found" });
                }
                var result = await userManager.DeleteAsync(deleteItem);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<Account> GetAccountById(string idAccount)
        {
            try
            {
                var account = await userManager.FindByIdAsync(idAccount);
                if (account == null)
                {
                    return null;
                }
                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<List<AccountWithRoles>> GetAllAccountsAsync()
        {
            try
            {
                var lstAccount = await userManager.Users.ToListAsync();
                var accountsWithRoles = new List<AccountWithRoles>();

                foreach (var account in lstAccount)
                {
                    var roles = await userManager.GetRolesAsync(account);
                    accountsWithRoles.Add(new AccountWithRoles
                    {
                        Account = account,
                        Roles = roles.ToList()
                    });
                }

                return accountsWithRoles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<IdentityResult> ToggleLockAccountAsync(string idAccount)
        {
            try
            {
                var account = await userManager.FindByIdAsync(idAccount);
                if (account == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Account not found" });
                }
                if (account.LockoutEnd != null && account.LockoutEnd > DateTimeOffset.UtcNow)
                {
                    account.LockoutEnd = null;
                }
                else
                {
                    account.LockoutEnd = DateTimeOffset.UtcNow.AddHours(1); // khóa trong vòng 1 giờ
                }
                return await userManager.UpdateAsync(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> GetPasswordHashByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }
            return user.PasswordHash;
        }

    }
}
