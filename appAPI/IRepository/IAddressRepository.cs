using appAPI.Models;

namespace appAPI.IRepository
{
    public interface IAddressRepository
    {
        public Task<List<Address>> GetAddressByUserId(long userId);
        public Task CreateAddress(Address address);
        public Task UpdateAddress(Address address, long id);
        public Task DeleteAddress(long id);

    }
}
