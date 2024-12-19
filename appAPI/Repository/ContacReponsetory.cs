using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class ContacReponsetory : IContactReponsetory
    {
        APP_DATA_DATN _context;
        public ContacReponsetory(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Contact c)
        {
            c.CreatedAt = DateTime.Now;
            c.Deleted = false;
            _context.Contacts.Add(c);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Contact c)
        {
            var itemud = await _context.Contacts.FindAsync(c.Id);
            if(itemud != null)
            {
                itemud.UpdatedAt = DateTime.Now;
                itemud.Replies = c.Replies;
                _context.Contacts.Update(itemud);
                await _context.SaveChangesAsync();

            }

        }
        public async Task Delete(long id)
        {
            var item = _context.Contacts.Find(id);
            if (item != null)
            {
                item.Deleted = true;
                item.DeletedAt = DateTime.Now;
                _context.Contacts.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        } 
        public Task<Contact> GetById(long id)
        {
                return _context.Contacts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Contact>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
                return await _context.Contacts
                 .Where(p =>   p.Deleted == false && (string.IsNullOrEmpty(searchTerm) || p.FullName.Contains(searchTerm) || p.Phone.Contains(searchTerm)))
                 .OrderBy(p => p.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            return await _context.Contacts
               .CountAsync(p =>   p.Deleted == false && (string.IsNullOrEmpty(searchTerm) || p.FullName.Contains(searchTerm) || p.Phone.Contains(searchTerm)));
        }

     
    }
}
