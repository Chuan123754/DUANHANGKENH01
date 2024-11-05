using ClientViews.IServices;
using ClientViews.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientViews.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        } 

        public async Task<List<Users>> GetAll()
        {
            string requestURL = "https://localhost:7011/api/Users/Users-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Users>>(response);
        }

        public async Task<Users> GetById(long id)
        {
            string requestURL = $"https://localhost:7011/api/Users/Users-get-id?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Users>(response);
        }

        public async Task Create(Users user)
        {
            user.Id = 0; 
            user.Created_at = DateTime.Now;
            user.Updated_at = DateTime.Now;

            // Kiểm tra trùng lặp email
            if (await IsEmailExists(user.Email))
            {
                throw new Exception("Email đã tồn tại.");
            }

            // Kiểm tra trùng lặp số điện thoại
            if (await IsPhoneExists(user.Phone))
            {
                throw new Exception("Số điện thoại đã tồn tại.");
            }

            string userRequestURL = "https://localhost:7011/api/Users/Users-post";
            var userJsonContent = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJsonContent, Encoding.UTF8, "application/json");
            var userResponse = await _httpClient.PostAsync(userRequestURL, userContent);

            if (!userResponse.IsSuccessStatusCode)
            {
                var errorContent = await userResponse.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status code {userResponse.StatusCode} and message: {errorContent}");
            }

            // Lấy đối tượng người dùng từ phản hồi
            var createdUserJson = await userResponse.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<Users>(createdUserJson);

            if (createdUser == null)
            {
                throw new Exception("Không thể lấy được người dùng vừa tạo.");
            }

            var cart = new Carts
            {
                Id = 0,
                UserId = createdUser.Id, 
                Status = "New",
                Description = "Giỏ hàng mặc định"
            };

            string cartRequestURL = "https://localhost:7011/api/Carts/carts-post";
            var cartJsonContent = JsonConvert.SerializeObject(cart);
            var cartContent = new StringContent(cartJsonContent, Encoding.UTF8, "application/json");
            var cartResponse = await _httpClient.PostAsync(cartRequestURL, cartContent);

            if (!cartResponse.IsSuccessStatusCode)
            {
                var errorContent = await cartResponse.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status code {cartResponse.StatusCode} and message: {errorContent}");
            }
        }

        public async Task Update(Users user)
        {
            string requestURL = "https://localhost:7011/api/Users/Users-put";
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(requestURL, content);
        }

        public async Task Delete(long id)
        {
            string requestURL = $"https://localhost:7011/api/Users/Users-delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }


        public async Task<bool> IsEmailExists(string email)
        {
            try
            {
                string requestURL = $"https://localhost:7011/api/Users/Users-get-email?email={email}";
                var response = await _httpClient.GetStringAsync(requestURL);
                var user = JsonConvert.DeserializeObject<Users>(response);
                return user != null;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Nếu mã trạng thái là 404, thì email chưa tồn tại
                return false;
            }
        }

        public async Task<bool> IsPhoneExists(string phone)
        {
            try
            {
                string requestURL = $"https://localhost:7011/api/Users/Users-get-phone?phone={phone}";
                var response = await _httpClient.GetStringAsync(requestURL);
                var user = JsonConvert.DeserializeObject<Users>(response);
                return user != null;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Nếu mã trạng thái là 404, thì số điện thoại chưa tồn tại
                return false;
            }
        }
    }
}
