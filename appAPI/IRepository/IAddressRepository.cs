using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IAddressRepository
    {
        public Task<List<Address>> GetAll();
        public Task<List<Address>> GetAddressByUserId(long userId);
        public Task<Address> GetAddressById(long id);
        public Task CreateAddress(Address address);
        public Task<Address> CreateAddressAndReturn(Address address);
        public Task UpdateAddress(Address address, long id);
        public Task DeleteAddress(long id);
        public Task SetAsDefault(long id , Address address);

    }
}
