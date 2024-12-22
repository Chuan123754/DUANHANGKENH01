using ViewsFE.IServices;
using ViewsFE.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ViewsFE.Services
{
    public class CartDetailsService : ICartDetailsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CartDetailsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        public async Task<List<Cart_details>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/CartDetails/cartdetails-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Cart_details>>(response);
        }

        public async Task<Cart_details> GetById(long id)
        {
            string requestURL = $"{_baseUrl}/api/CartDetails/cartdetails-get-id/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Cart_details>(response);
        }

        public async Task Create(Cart_details cartDetails)
        {
            var cart = await FetchCart(cartDetails.Cart_id);
            var user = await FetchUser(cart.UserId);
            var product = await FetchProduct(cartDetails.Product_id);

            var cartDetailsToSend = PrepareCartDetails(cartDetails, cart, user, product);
            string requestURL = $"{_baseUrl}/api/CartDetails/cartdetails-post";
            await SendPostRequest(requestURL, cartDetailsToSend);
        }

        public async Task Update(Cart_details cartDetails)
        {
            var cart = await FetchCart(cartDetails.Cart_id);
            var user = await FetchUser(cart.UserId);
            var product = await FetchProduct(cartDetails.Product_id);

            var cartDetailsToSend = PrepareCartDetails(cartDetails, cart, user, product);
            string requestURL = $"{_baseUrl}/api/CartDetails/cartdetails-put";
            await SendPutRequest(requestURL, cartDetailsToSend);
        }

        public async Task Delete(long id)
        {
            string requestURL = $"{_baseUrl}/api/CartDetails/cartdetails-delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed with status code {response.StatusCode}");
            }
        }

        private async Task<Carts> FetchCart(long cartId)
        {
            string requestURL = $"{_baseUrl}/api/Carts/carts-get-id/{cartId}";
            var response = await _httpClient.GetAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Giỏ hàng không tồn tại.");
            }

            var cartJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Carts>(cartJson);
        }

        private async Task<Users> FetchUser(long userId)
        {
            string requestURL = $"{_baseUrl}/api/Users/Users-get-id?id={userId}";
            var response = await _httpClient.GetAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Người dùng không tồn tại.");
            }

            var userJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Users>(userJson);
        }

        private async Task<Product_Attributes> FetchProduct(long productId)
        {
            string requestURL = $"{_baseUrl}/api/PostProducts/postproducts-get-id/{productId}";
            var response = await _httpClient.GetAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Sản phẩm không tồn tại.");
            }

            var productJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product_Attributes>(productJson);
        }

        private object PrepareCartDetails(Cart_details cartDetails, Carts cart, Users user, Product_Attributes product)
        {
            return new
            {
                Id = cartDetails.Id,
                Product_id = cartDetails.Product_id,
                Cart_id = cartDetails.Cart_id,
                Quantity = cartDetails.Quantity,
                Status = cartDetails.Status,
                Carts = new
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
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
                        Created_at = user.Created_at,
                        Updated_at = user.Updated_at
                    }
                }
            };
        }

        private async Task SendPostRequest(string requestURL, object data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API call failed with status code {response.StatusCode} and message: {errorContent}");
            }
        }

        private async Task SendPutRequest(string requestURL, object data)
        {
            string jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API call failed with status code {response.StatusCode} and message: {errorContent}");
            }
        }
    }
}
