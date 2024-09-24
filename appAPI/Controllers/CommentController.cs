using AppAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        CommentRepository _commentRepo;
        public CommentController()
        {
            _commentRepo = new CommentRepository();
        }
        [HttpGet("GetAll")]
        public Task<List<Comments>> GetAll()
        {
            return _commentRepo.GetAll();
        }
        [HttpGet("GetById")]
        public Task<Comments> GetById(long id)
        {
            return _commentRepo.GetById(id);
        }
        [HttpPost("Create")]
        public Task<Comments> Create(Comments comment)
        {
            return _commentRepo.Create(comment);
        }
        [HttpPut("Update")]
        public Task<Comments> Update(long id, Comments comment)
        {
            return _commentRepo.Update(id, comment);
        }
        [HttpDelete("Delete")]
        public Task Delete(long id)
        {
            return _commentRepo.Delete(id);
        }
    }

}
