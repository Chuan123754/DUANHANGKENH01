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
        public async Task<List<Address>> GetAll()
        {
            var lstAddress =  _context.Address.ToList();
            return lstAddress;
        }
        public async Task<Address> GetAddressById(long id)
        {
            var address = _context.Address.Find(id);
            return address;
        
        }
        public async Task<List<Address>> GetAddressByUserId(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                var lst_Address = await _context.Address
                         .Where(x => x.User_Id == user.Id)
                         .Include(x=>x.User)
                         .ToListAsync();
                       
                         
                return lst_Address;
            }
            return new List<Address>(); // Trả về danh sách rỗng nếu không tìm thấy người dùng

        }
        public async Task CreateAddress(Address address)
        {
            address.Set_as_default = 1;

            _context.Address.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> CreateAddressAndReturn(Address address)
        {
            address.Set_as_default = 1;
            _context.Address.Add(address);
            await _context.SaveChangesAsync();
            return address; // Trả về địa chỉ vừa được thêm
        }


        public async Task DeleteAddress(long id)
        {
            var deleteItem = await _context.Address.FindAsync(id);
            _context.Address.Remove(deleteItem);
            _context.SaveChanges();
        }

        public async Task UpdateAddress(Address address, long id)
        {
            var updateItem = await _context.Address.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.Name = address.Name;
                updateItem.WardCode = address.WardCode;
                updateItem.DistrictId = address.DistrictId;
                updateItem.Phone = address.Phone;
                updateItem.Email = address.Email;
                updateItem.Street = address.Street;
                updateItem.Ward_commune = address.Ward_commune;
                updateItem.District = address.District;
                updateItem.Province_city = address.Province_city;
            }
            _context.Address.Update(updateItem);
            await _context.SaveChangesAsync();
        }
        public async Task SetAsDefault(long id, Address address)
        {
            var addressList = await _context.Address.Where(a => a.Set_as_default == 0 && a.User_Id == id).ToListAsync();

            // Đặt tất cả các địa chỉ không mặc định thành địa chỉ bình thường
            foreach (var item in addressList)
            {
                item.Set_as_default = 1;
            }
            // địa chỉ được chọn thành địa chỉ mặc định
            address = await _context.Address.FindAsync(id);
            if (address != null)
            {
                address.Name = "Địa chỉ mặc định";
                address.Set_as_default = 0;
            }

            await _context.SaveChangesAsync();
        }
    }
}
