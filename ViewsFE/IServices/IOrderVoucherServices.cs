using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IOrderVoucherServices
    {
        Task GetByVoucherIdAndOrderId(long orderId, long voucherId);
        Task<bool> Create(Order_Vouchers order_Voucher);
    }
}
