using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using appAPI.Models;
using System.Threading.Tasks;
using appAPI.Repository;
using System.Net.Http;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FilesReponsetory _filesRepository;

        public FilesController()
        {
            _filesRepository = new FilesReponsetory();
        }

        [HttpGet("get-all-files")]
        public async Task<IActionResult> GetAll()
        {
            var lstFile = await _filesRepository.GetAll();
            return Ok(lstFile);
        }

        [HttpGet("get-details")]
        public async Task<IActionResult> Details(long id)
        {
            var file = await _filesRepository.GetById(id);
            if (file == null)
            {
                return NotFound("Không tồn tại file này");
            }
            return Ok(file);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                await _filesRepository.Upload(file);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var file = await _filesRepository.GetById(id);
                if (file == null)
                {
                    return NotFound("Tệp không tồn tại.");
                }
                await _filesRepository.Delete(id);
                return Ok("Xóa tệp thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa tệp: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var result = await _filesRepository.Search(query);
            return Ok(result);
        }
    }
}
