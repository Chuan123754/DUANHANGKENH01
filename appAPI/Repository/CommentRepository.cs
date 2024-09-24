using AppAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class CommentRepository: ICommentRepository
    {
        APP_DATA_DATN _context;
        public CommentRepository()
        {
            _context = new APP_DATA_DATN();
        }
        public async Task<List<Comments>> GetAll()
        {
            var lst_Comments = await _context.Comments.Select(x=> new Comments
            {
                Id = x.Id,
                Post_id = x.Post_id,
                User_id = x.User_id,
                User_admin_id = x.User_admin_id,
                Content = x.Content,
                Author_IP = x.Author_IP,
                Parent = x.Parent,
                Type = x.Type,
                Status = x.Status,
                Created_at = x.Created_at,
                Updated_at = x.Updated_at,

            }).ToListAsync();
            return lst_Comments;
        }
        public async Task<Comments> GetById(long id)
        {
            return await _context.Comments.FindAsync(id);
        }
        public async Task<Comments> Create (Comments comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();  
            return comment;
        }
        public async Task<Comments> Update(long id, Comments comment)
        {
            var updateItem = await _context.Comments.FindAsync(id);
            if (updateItem == null) { return null; };
            updateItem.Content = comment.Content;
            updateItem.Author_IP = comment.Author_IP;
            updateItem.Parent = comment.Parent;
            updateItem.Type = comment.Type;
            updateItem.Status = comment.Status;
            updateItem.Updated_at = DateTime.Now;

            _context.Comments.Update(updateItem);
            _context.SaveChangesAsync();
            return comment;
        }
        public async Task<Comments> Delete(long id)
        {
            var deleteItem = await _context.Comments.FindAsync(id);
            if (deleteItem == null) { return null; }
            _context.Comments.Remove(deleteItem);
            await _context.SaveChangesAsync();
            return deleteItem;
        } 
    }
}
