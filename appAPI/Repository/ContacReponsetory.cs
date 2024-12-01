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
            _context.Contacts.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var item = _context.Contacts.Find(id);
            if (item != null)
            {
                item.DeletedAt = DateTime.Now;
                _context.Contacts.Remove(item);
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
                 .Where(p => (string.IsNullOrEmpty(searchTerm) || p.FullName.Contains(searchTerm) || p.Phone.Contains(searchTerm)))
                 .OrderBy(p => p.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            }

        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            return await _context.Contacts
               .CountAsync(p => (string.IsNullOrEmpty(searchTerm) || p.FullName.Contains(searchTerm) || p.Phone.Contains(searchTerm)));
        }
    }
}
