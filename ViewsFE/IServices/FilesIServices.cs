using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface FilesIServices
    {
        Task<List<Files>> GetAll();
        Task<Files> GetById(long id);
        Task<object> Upload(MultipartFormDataContent content);
        Task Delete(long id);
        Task<List<Files>> Search(string keyword);
    }
}