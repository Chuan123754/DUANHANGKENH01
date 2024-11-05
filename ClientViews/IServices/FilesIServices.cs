using Microsoft.AspNetCore.Components.Forms;
using ClientViews.Models;

namespace ClientViews.IServices
{
    public interface FilesIServices
    {
        Task<List<Files>> GetAll();
        Task<Files> GetById(long id);
        Task<object> Upload(IBrowserFile file);
        Task Delete(long id);
        Task<List<Files>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string searchTerm);
    }
}