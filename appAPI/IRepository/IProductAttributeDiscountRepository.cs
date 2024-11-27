using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IProductAttributeDiscountRepository
    {
        Task<List<P_attribute_discount>> GetAll();
        Task<P_attribute_discount> GetByIdAndType(long id);
        Task<List<P_attribute_discount>> GetByIdDiscount(long idDiscount);
        Task<List<P_attribute_discount>> GetByIdProduct(long idProduct);
        Task Create(P_attribute_discount attributeDiscount);
        Task Update(P_attribute_discount attributeDiscount,long id);
        Task Delete(long id);
    }
}
