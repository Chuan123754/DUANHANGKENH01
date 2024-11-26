using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IAttributesDiscountServices
    {
        Task<List<P_attribute_discount>> GetAll();
        Task<List<Product_Attributes>> GetProductvariants();
        Task<List<Discount>> GetDiscount();
        Task<P_attribute_discount> GetByIdAndType(long id);
        Task<List<P_attribute_discount>> GetByIdDiscount(long idDiscount);
        Task<List<P_attribute_discount>> GetByIdProduct(long idProduct);
        Task<P_attribute_discount> Details (long id);
        Task<P_attribute_discount> Create (P_attribute_discount vd);
        Task<bool> Update (P_attribute_discount vd);
        Task<bool> Delete (long id);
    }
}
