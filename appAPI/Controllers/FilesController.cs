using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using appAPI.Models;
using System.Threading.Tasks;
using appAPI.Repository;
using System.Net.Http;
using appAPI.IRepository;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FilesIRepository _filesRepository;

        public FilesController(FilesIRepository filesIRepository)
        {
            _filesRepository = filesIRepository;
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
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 21, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page file must be greater than 0.");
            }

            var list = await _filesRepository.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _filesRepository.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
        
    }
}
