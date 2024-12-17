using appAPI.Models;
using appAPI.Repository;
using appAPI.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignerController : ControllerBase
    {
        private readonly IDesignerRepon _repon;
        APP_DATA_DATN _context;
        public DesignerController(APP_DATA_DATN context,IDesignerRepon repon)
        {
            _repon = repon;
            _context = context;
        }
        [HttpGet("checkslug")]
        public async Task<IActionResult> CheckSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return BadRequest("Slug không được để trống.");
            }

            bool exists = await _context.Designer.Where(p => p.Deleted == false).AnyAsync(x => x.slug == slug);
            return Ok(!exists);
        }
        [HttpGet("check-slug-for-update")]
        public async Task<IActionResult> CheckSlugForUpdate([FromQuery] string slug, [FromQuery] long desiId)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return BadRequest(new { message = "Slug không được để trống." });
            }

            try
            {
                bool isUnique = await _repon.CheckSlugForUpdate(slug, desiId);

                return Ok(new { isUnique }); // Trả về true nếu Slug hợp lệ, false nếu trùng
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi xảy ra: {ex.Message}" });
            }
        }
        // GET: api/Designer
        [HttpGet]
        public async Task<List<Designer>> GetAll()
        {
            return await _repon.GetAll();
        }
        [HttpGet("GetAllAC")]
        public async Task<List<Designer>> GetAllAC()
        {
            return await _repon.GetAllAC();
        }
        // GET: api/Designer/{id}
        [HttpGet("{id}")]
        public async Task<Designer> GetById(long id)
        {
            return await _repon.GetById(id);
        }
        [HttpGet("GetByIdSlug")]
        public async Task<Designer> GetByIdSlug(string slug)
        {
            return await _repon.GetByIdSlug(slug);
        }
        // POST: api/Designer
        [HttpPost]
        public async Task<IActionResult> Post(Designer d)
        {
           var createDS = await _repon.Create(d);
            return Ok(new { message = "Thêm nhà thiết kế thành công", Post_Id = createDS.id_Designer });
        }

        // PUT: api/Designer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Designer d)
        {
            //d.Id = id; // Đảm bảo đối tượng `d` có `id` cần cập nhật
            var edirDS = await _repon.Update(d);
            return Ok(new { message = "Sửa nhà thiết kế thành công", Post_Id = edirDS.id_Designer });
        }

        // DELETE: api/Designer/{id}
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _repon.Delete(id);
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var result = await _repon.GetByTypeAsync(pageNumber, pageSize, searchTerm);
            return Ok(result);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repon.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
        [HttpGet("get-by-type-client")]
        public async Task<IActionResult> GetByTypeClient([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var result = await _repon.GetByTypeAsyncClient(pageNumber, pageSize, searchTerm);
            return Ok(result);
        }
        [HttpGet("Get-Total-Count-Client")]
        public async Task<IActionResult> GetTotalCountClient([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repon.GetTotalCountAsyncClient(searchTerm);
            return Ok(totalCount);
        }
    }
}
