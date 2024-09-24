using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IAttributesRepon
    {
        Task<List<Attributes>> GetAll();
        Task<Attributes> GetById(long id);
        Task Create(Attributes at);
        Task Update(Attributes at);
        Task Delete(long id);
        Task<List<Attributes>> Search(string keyword);
    }
}
