using AppViews.IServices;
using System.Text;
using Views.Models;
using Newtonsoft.Json;

namespace Views.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CartService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        private async Task<Users> GetUserById(long userId)
        {
            string userRequestUrl = $"{_baseUrl}/api/Users/Users-get-id?id={userId}";
            var userResponse = await _httpClient.GetAsync(userRequestUrl);

            if (!userResponse.IsSuccessStatusCode)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            var userJson = await userResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Users>(userJson);
        }

        public async Task<List<Carts>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/Carts/carts-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Carts>>(response);
        }

        public async Task<Carts> GetById(long id)
        {
            string requestURL = $"{_baseUrl}/api/Carts/carts-get-id/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Carts>(response);
        }

        public async Task Create(Carts cart)
        {
            var user = await GetUserById(cart.Users.Id);

            var cartToSend = new
            {
                Id = cart.Id,
                Status = cart.Status,
                Description = cart.Description,
                Users = new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    EmailVerifiedAt = user.EmailVerifiedAt,
                    Password = user.Password,
                    RememberToken = user.RememberToken,
                    Address = user.Address,
                    CreatedAt = user.Created_at,
                    UpdatedAt = user.Updated_at
                },
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };

            string jsonContent = JsonConvert.SerializeObject(cartToSend);

            string requestURL = $"{_baseUrl}/api/Carts/carts-post";
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Tạo giỏ hàng thất bại với mã trạng thái {response.StatusCode} và thông điệp: {errorContent}");
            }
        }

        public async Task Update(Carts cart)
        {
            var user = await GetUserById(cart.Users.Id);

            var cartToSend = new
            {
                Id = cart.Id,
                Status = cart.Status,
                Description = cart.Description,
                Users = new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    EmailVerifiedAt = user.EmailVerifiedAt,
                    Password = user.Password,
                    RememberToken = user.RememberToken,
                    Address = user.Address,
                    CreatedAt = user.Created_at,
                    UpdatedAt = user.Updated_at
                },            
                UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };

            string jsonContent = JsonConvert.SerializeObject(cartToSend);

            string requestURL = $"{_baseUrl}/api/Carts/carts-put";
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Cập nhật giỏ hàng thất bại với mã trạng thái {response.StatusCode} và thông điệp: {errorContent}");
            }
        }

        public async Task Delete(long id)
        {
            string requestURL = $"{_baseUrl}/api/Carts/carts-delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Xóa giỏ hàng thất bại với mã trạng thái {response.StatusCode}");
            }
        }
    }
}
