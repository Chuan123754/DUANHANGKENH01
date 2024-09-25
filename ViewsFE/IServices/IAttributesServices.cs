using Views.Models;

namespace Views.IServices
{
    public interface IAttributesServices
    {
        Task<List<Attributes>> GetAll();
        Task<Attributes> GetById(long id);
        Task Create(Attributes cartDetails);
        Task Update(Attributes cartDetails);
        Task Delete(long id);
        Task<List<Attributes>> Search(string keyword);        
    }
}
