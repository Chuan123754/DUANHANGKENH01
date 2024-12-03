using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IPaymentRepository
    {
        public Task<List<Payment>> GetAll();
        public Task Create(Payment payment);
    }
}
