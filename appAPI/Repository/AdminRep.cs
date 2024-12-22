//using appAPI.IRepository;
//using appAPI.Models;
//using Microsoft.EntityFrameworkCore;

//namespace appAPI.Repository
//{
//    public class AdminRep : IAdminRepo
//    {
//        private readonly APP_DATA_DATN _context;

//        public AdminRep(APP_DATA_DATN context)
//        {
//            _context= context;
//        }
//        public async Task Create(Admin admin)
//        {
//            _context.Admins.Add(admin);
//            await _context.SaveChangesAsync();
//        }

//        public async Task Delete(long id)
//        {
//            var admin = await _context.Admins.FindAsync(id);
//            if (admin != null)
//            {
//                _context.Admins.Remove(admin);
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<List<Admin>> GetAll()
//        {
//            return await _context.Admins.ToListAsync();
//        }

//        public async Task<Admin> GetById(long id)
//        {
//            return await _context.Admins.FindAsync(id);
//        }

//        public async Task<Admin> Signin(Admin admin)
//        {
//            var existingAdmin = await _context.Admins
//               .FirstOrDefaultAsync(a => a.UserName == admin.UserName && a.Password ==admin.Password);

//            return existingAdmin;
//        }


//        public async Task Signup(Admin admin)
//        {
//            _context.Admins.Add(admin);
//            await _context.SaveChangesAsync();
//        }

//        public async Task Update(Admin admin, long id)
//        {
//            var existingAdmin = await _context.Admins.FindAsync(id);
//            if (existingAdmin != null)
//            {
//                existingAdmin.FirstName = admin.FirstName;
//                existingAdmin.LastName = admin.LastName;
//                existingAdmin.PhoneNumber = admin.PhoneNumber;
//                existingAdmin.Email = admin.Email;
//                existingAdmin.UserName = admin.UserName;
//                existingAdmin.Password = admin.Password; // Mã hóa mật khẩu  
//                existingAdmin.DateOfBirth = admin.DateOfBirth;
//                existingAdmin.Role = admin.Role;

//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}
