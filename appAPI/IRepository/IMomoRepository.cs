using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IMomoRepository
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
    }
}
