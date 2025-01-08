using appAPI.DTO;
using appAPI.Models;
using appAPI.Models.DTO;

namespace appAPI.IRepository
{
    public interface IProductAttributesRepository
    {
        Task<List<Product_Attributes>> GetAllProductAttributes();
        Task<List<Product_Attributes>> GetAllProductAttributesDelete();
        Task<List<Product_Attributes>> GetProductAttributesByPostId(long id);
        Task<List<Product_Attributes>> GetProductAttributesByPostIdClient(long id);
        Task<Product_Attributes> GetProductAttributesById(long id);
        Task Create(Product_Attributes productAttribute);
        Task Update(Product_Attributes productAttribute ,long id);
        Task Delete(long id);
        Task DeleteCung(long id);
        // lấy danh sách biến thể dựa trên id sản phảm(product_variant)
        //Task<List<Product_Attributes_DTO>> GetVariantByProductVariantId(List<long> variantIds);
        //Task<List<ProductDTO>> GetProductDTOsAsync();
        Task<List<Product_Attributes>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);

        Task Restore(long id);
        Task BatchUpdateAsync(IEnumerable<Product_Attributes> productAttributes);

    }
}
