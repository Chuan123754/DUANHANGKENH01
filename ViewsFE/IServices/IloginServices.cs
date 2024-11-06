using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IloginServices
    {
        Task<string> SignUp(SignUpModel model);  
        Task<string> Login(SignInModel model);   
        Task<bool> SignOut();
    }
}
