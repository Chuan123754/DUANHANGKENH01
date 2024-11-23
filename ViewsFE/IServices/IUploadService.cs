using System.Collections.Generic;
using System.Threading.Tasks;
using ViewsFE.Models;
using static ViewsFE.Pages.Admin.Size.Index;

namespace ViewsFE.IServices
{
    public interface IUploadService
    {
        Task<UploadResponse> UploadExcel(string tableName, IFormFile file);
        Task<byte[]> ExportExcel(string tableName);
    }
}
