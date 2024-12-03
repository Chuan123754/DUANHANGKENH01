using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly APP_DATA_DATN _context;

        public PaymentRepository(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();  
        }

        public async Task<List<Payment>> GetAll()
        {
             return _context.Payment.ToList();
        }
    }
}
