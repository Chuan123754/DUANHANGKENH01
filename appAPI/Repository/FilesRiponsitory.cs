using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using static AppAPI.Base.Dictionary;

namespace appAPI.Repository
{
    public class FilesReponsetory : FilesIRepository
    {
        private readonly APP_DATA_DATN _context;
        private string _getfilePath = $@"https://localhost:7007/FileMedia/";
        private string _uploadFolderPath = $@"D:\DATN\DATNHK\AppAPI\FileMedia";

        public FilesReponsetory()
        {
            _context = new APP_DATA_DATN();
        }

        public async Task Delete(long id)
        {
            var fileItem = await GetById(id);
            _context.Files.Remove(fileItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Files>> GetAll()
        {
            var lstFile = await _context.Files.Select(x => new Files
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Mine = x.Mine,
                Size = x.Size,
                Ext = x.Ext,
                Path = _getfilePath + x.Name,
                Created_at = x.Created_at,
                Updated_at = x.Updated_at
            }).ToListAsync();

            return lstFile;
        }

        public async Task<Files> GetById(long id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task<List<Files>> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return new List<Files>();
            }

            return await _context.Files
                .Where(f => f.Name.Contains(keyword) || f.Slug.Contains(keyword))
                .ToListAsync();
        }

        public async Task Upload(MultipartFormDataContent content)
        {
            if (content == null || !content.Any())
            {
                throw new ArgumentException("Không có dữ liệu nào được tải lên");
            }

            var fileContent = content.FirstOrDefault(c => c.Headers.ContentDisposition != null && c.Headers.ContentDisposition.DispositionType.Equals("form-data"));
            if (fileContent == null)
            {
                throw new ArgumentException("Không tìm thấy tệp trong dữ liệu tải lên");
            }

            var fileName = ContentDispositionHeaderValue.Parse(fileContent.Headers.ContentDisposition.ToString()).FileName.Trim('"');
            var filePath = Path.Combine(_uploadFolderPath, fileName);

            // Lưu file vào server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileContent.CopyToAsync(stream);
            }

            string mime = GetMimeType(Path.GetExtension(fileName));

            var newFile = new Files
            {
                Name = fileName,
                Slug = Path.GetFileNameWithoutExtension(fileName),
                Mine = mime,
                Size = (int)(new FileInfo(filePath).Length / 1024),  // Kích thước file tính bằng KB
                Ext = Path.GetExtension(fileName),
                Path = filePath,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            await _context.Files.AddAsync(newFile);
            await _context.SaveChangesAsync();
        }
    }
}
