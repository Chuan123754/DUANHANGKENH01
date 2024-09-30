using appAPI.Models;

namespace appAPI.IRepository
{
    public interface FilesIRepository
    {
        Task<List<Files>> GetAll();
        Task<Files> GetById(long id);
        Task Upload(MultipartFormDataContent content);
        Task Delete(long id);
        Task<List<Files>> Search(string keyword);
    }
}
