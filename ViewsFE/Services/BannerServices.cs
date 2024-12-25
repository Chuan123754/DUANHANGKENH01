using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class BannerServices : IBannerServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public BannerServices(IConfiguration configuration)
        {
            _client = new HttpClient();
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task AddBannerDesiner(long postId, Banner banner)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Banner/AddBannerDesiner?postId={postId}", banner);
        }

        public async Task AddBannerToPost(long postId, Banner banner)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Banner/CreateBannerPost?postId={postId}", banner);
        }

        public async Task Create(Banner banner)
        {
            await _client.PostAsJsonAsync($"{_baseUrl}/api/Banner/CreateBanner", banner);
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Banner>> GetAllBanner()
        {
            throw new NotImplementedException();
        }

        public async Task<Banner> GetBannerByDesignerId(long PostId)
        {
            return await _client.GetFromJsonAsync<Banner>($"{_baseUrl}/api/Banner/GetBannerByDesignerId?PostId={PostId}");
        }

        public Task<Banner> GetBannerById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Banner> GetBannerByProductPostId(long PostId)
        {
            return await _client.GetFromJsonAsync<Banner>($"{_baseUrl}/api/Banner/GetBannerByProductPostId?PostId={PostId}");
        }

        public async Task Update(Banner banner, long postId)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Banner/UpdateBanner?postId={postId}", banner);
        }

        public async Task UpdateToDesiner(Banner banner, long postId)
        {
            await _client.PutAsJsonAsync($"{_baseUrl}/api/Banner/UpdateToDesiner?postId={postId}", banner);
        }
    }
}
