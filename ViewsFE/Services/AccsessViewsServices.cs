using ViewsFE.IServices;

namespace ViewsFE.Services
{
    public class AccsessViewsServices : IAccsessViewscsServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public AccsessViewsServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }
        public async Task<long> GetTotal()
        {
            var url = $"{_baseUrl}/api/Accsess/GetTotal";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Kiểm tra xem phản hồi có thành công hay không

            var count = await response.Content.ReadFromJsonAsync<int>();
            return count;
        }
    }
}
