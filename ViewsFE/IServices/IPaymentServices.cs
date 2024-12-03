using appAPI.Models;

namespace ViewsFE.IServices
{
    public interface IPaymentServices
    {
        public Task<List<Payment>> GetAll();
        //public Task Create(Payment payment);
    }
}
