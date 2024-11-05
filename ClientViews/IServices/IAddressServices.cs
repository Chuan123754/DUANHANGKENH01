using ClientViews.Models;

namespace ClientViews.IServices
{
    public interface IAddressServices
    {
        public Task<List<Address>> GetAll();
        public Task<List<Address>> GetAddressByUserId(long userId);
        public Task<Address> GetAddressById(long id);
        public Task CreateAddress(Address address);
        public Task UpdateAddress(Address address, long id);
        public Task DeleteAddress(long id);
        public Task SetAsDefault(long id, Address address);
    }
}
