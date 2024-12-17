using ViewsFE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ViewsFE.Pages.Admin.Dashboard.Dashboard;

namespace ViewsFE.IServices
{
    public interface IUserService
    {
        Task<List<Users>> GetAll();
        Task<Users> GetById(long id);
        Task<Users> Create(Users user);
        Task Register(Users user);
        Task<Users> Login(Users user, bool rememberMe);
        Task<Users> AutoLogin(string rememberToken);
        Task Logout(long idUser);  
        Task Update(Users user);
        Task Delete(long id);
        Task<bool> IsEmailExists(string email); // Kiểm tra email
        Task<bool> IsPhoneExists(string phone); // Kiểm tra số điện thoại
        Task<Users> GetByPhoneNumber(string phone);
        Task<bool> UpdatePassword(long userId, string newPassword);
        Task<int> GetTotalUsersByDayAsync();
        Task<int> GetTotalUsersByMonthAsync();
        Task<int> GetTotalUsersByYearAsync();
        Task<int> GetTotalUsersAsync();
        Task<List<CustomerDto>> GetTop5CustomersAsync();
        Task<List<CustomerDto>> GetTop5CustomersWeeklyAsync();
        Task<List<CustomerDto>> GetTop5CustomersMonthlyAsync();
        Task<List<CustomerDto>> GetTop5CustomersYearlyAsync();
    }
}
