using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class AddressRepository: IAddressRepository
    {
        private readonly APP_DATA_DATN _context;
        private readonly UserManager<Account> _userManager;

        public AddressRepository( APP_DATA_DATN context , UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateAddress(Address address)
        {
             _context.Address.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddress(long id)
        {
            var deleteItem = await _context.Address.FindAsync(id);
            _context.Address.Remove(deleteItem);
            _context.SaveChanges();
        }

        public async Task<List<Address>> GetAddressByUserId(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                var lst_Address = await _context.Address
                         .Where(x => x.User_Id == user.Id)
                         .Select(x => new Address
                {
                    Id = x.Id,
                    User_Id = x.User_Id,
                    Name = x.Name,
                    Street = x.Street,
                    Ward_commune = x.Ward_commune,
                    District = x.District,
                    Province_city = x.Province_city,
                    Type = x.Type,
                    Set_as_default = x.Set_as_default,
                    Status = x.Status,
                }).ToListAsync();
                return lst_Address;
            }
            return new List<Address>(); // Trả về danh sách rỗng nếu không tìm thấy người dùng

        }

        public async Task UpdateAddress(Address address, long id)
        {
            var updateItem = await _context.Address.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.Name = address.Name;
                updateItem.Name = address.Street;
                updateItem.Name = address.Ward_commune;
                updateItem.Name = address.District;
                updateItem.Name = address.Province_city;
            }
            _context.Address.Update(updateItem);
            await _context.SaveChangesAsync();
        }
    }
}
