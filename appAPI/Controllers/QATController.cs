using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QATController : ControllerBase
    {
        private readonly IRepository<Q_A> _qaRepository;

        public QATController(IRepository<Q_A> qaRepository)
        {
            _qaRepository = qaRepository;
        }

        [HttpGet("QA-get")]
        public IActionResult Get()
        {
            return Ok(_qaRepository.GetAll());
        }

        [HttpGet("QA-get-id")]
        public IActionResult Get(long id)
        {
            var qa = _qaRepository.GetById(id);
            if (qa == null)
            {
                return NotFound("QA not found");
            }
            return Ok(qa);
        }

        [HttpPost("QA-post")]
        public IActionResult Post(Q_A qa)
        {
            try
            {
                qa.Created_at = DateTime.Now;
                _qaRepository.Add(qa);
                return Ok(new { message = "Thêm QA thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Đã xảy ra lỗi khi thêm QA", error = ex.Message });
            }
        }

        [HttpPut("QA-put")]
        public IActionResult Put(Q_A qa)
        {
            var item = _qaRepository.GetById(qa.Id);
            if (item == null)
            {
                return NotFound("QA not found");
            }

            item.Question = qa.Question;
            item.Answer = qa.Answer;
            item.Created_by = qa.Created_by;
            item.Updated_by = qa.Updated_by;
            item.Created_at = qa.Created_at;
            item.Updated_at = DateTime.Now;

            _qaRepository.Update(item);
            return Ok(new { message = "Cập nhật QA thành công" });
        }

        [HttpDelete("QA-delete")]
        public IActionResult Delete(long id)
        {
            var delete = _qaRepository.GetById(id);
            if (delete == null)
            {
                return NotFound("QA not found");
            }

            _qaRepository.Remove(delete);
            return Ok(new { message = "Xóa QA thành công" });
        }
    }
}
