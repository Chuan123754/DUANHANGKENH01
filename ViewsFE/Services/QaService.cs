using Views.Models;
using ViewsFE.IServices;

namespace ViewsFE.Services
{
    public class QaService : IQaService
    {
        private readonly HttpClient _httpClient;

        public QaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Q_A>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<Q_A>>("https://localhost:7011/api/QAT/QA-get");
        }

        public async Task<Q_A> GetById(long id)
        {
            return await _httpClient.GetFromJsonAsync<Q_A>($"https://localhost:7011/api/QAT/QA-get-id?id={id}");
        }

        public async Task Create(Q_A qa)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7011/api/QAT/QA-post", qa);
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(Q_A qa)
        {
            var response = await _httpClient.PutAsJsonAsync("https://localhost:7011/api/QAT/QA-put", qa);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(long id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7011/api/QAT/QA-delete?id={id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
