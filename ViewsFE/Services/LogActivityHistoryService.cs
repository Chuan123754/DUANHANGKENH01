using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public interface ILogActivityHistoryService
    {
        Task LogActivityAsync(string logName, string description, string subjectType, string accountId);
        Task<List<Activity_history>> GetAllActivitiesAsync();
        Task<List<Activity_history>> GetActivitiesByAccountIdAsync(string accountId);
    }
    public class LogActivityHistoryService :ILogActivityHistoryService
    {
        private readonly UserManager<Account> _userManager;
        private readonly APP_DATA_DATN _context;
        public LogActivityHistoryService(APP_DATA_DATN context, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _context =context;
        }
        public async Task LogActivityAsync(string logName, string description, string subjectType, string accountId)
        {
            try
            {
                if (string.IsNullOrEmpty(accountId))
                {
                    Console.WriteLine("accountId không hợp lệ");
                    return;
                }
                var activity = new Activity_history
                {
                        
                    Log_name = logName,
                    Descripcion = description,
                    Subject_type = subjectType,
                    Time = DateTime.Now,
                    AccountId = accountId
                };
                _context.Activity_history.Add(activity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi ghi log: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
      
        }
        public async Task<List<Activity_history>> GetAllActivitiesAsync()
        {
            return await _context.Activity_history
                .Include(a => a.Account) 
                .ToListAsync();
        }

        public async Task<List<Activity_history>> GetActivitiesByAccountIdAsync(string accountId)
        {
            return await _context.Activity_history
                .Where(a => a.AccountId == accountId)
                .Include(a => a.Account) 
                .ToListAsync();
        }
    }
}
