using Views.Models;

namespace AppViews.IServices
{
    public interface FilesIServices
    {
        Task<List<Files>> GetAll();
        Task<Files> GetById(long id);
        Task<object> Upload(IFormFile file);
        Task Delete(long id);
        Task<List<Files>> Search(string keyword);
    }
}