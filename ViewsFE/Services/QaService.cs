using ViewsFE.Models;
using ViewsFE.IServices;

namespace ViewsFE.Services
{
    public class QaService : IQaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public QaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<List<Q_A>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<Q_A>>($"{_baseUrl}/api/QAT/QA-get");
        }

        public async Task<Q_A> GetById(long id)
        {
            return await _httpClient.GetFromJsonAsync<Q_A>($"{_baseUrl}/api/QAT/QA-get-id?id={id}");
        }

        public async Task Create(Q_A qa)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/QAT/QA-post", qa);
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(Q_A qa)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/QAT/QA-put", qa);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(long id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/QAT/QA-delete?id={id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
