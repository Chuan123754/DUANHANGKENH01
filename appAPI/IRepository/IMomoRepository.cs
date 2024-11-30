using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IMomoRepository
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
        Task<MomoQueryPaymentResponseModel> QueryTransactionAsync(string orderId);
        Task UpdateOrderStatusAsync(string orderId, string status);

    }
}
