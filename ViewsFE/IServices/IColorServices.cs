using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IColorServices
    {
        Task<List<Color>> GetAll();
        Task<Color> Details(long id);
        Task Create(Color c);
        Task Update(Color c);
        Task Delete(long id);
        Task<List<Color>> Search(string keyword);
    }
}
