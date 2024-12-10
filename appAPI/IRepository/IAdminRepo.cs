using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IAdminRepo
    {
        public Task<List<Admin>> GetAll();
        public Task<Admin> GetById(long id);
        public Task Signup(Admin model);
        public Task<Admin> Signin(Admin model);
        public Task Create (Admin admin);
        public Task Update (Admin admin, long id);
        public Task Delete (long id);   
    }
}
