using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface IQaService
    {
        Task<List<Q_A>> GetAll();
        Task<Q_A> GetById(long id);
        Task Create(Q_A qa);
        Task Update(Q_A qa);
        Task Delete(long id);
    }
}
