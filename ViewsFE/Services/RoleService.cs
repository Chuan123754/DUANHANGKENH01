using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.JSInterop;
using ViewsFE.IServices;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ViewsFE.Services
{
    public class RoleService
    {
        private readonly AuthenticationStateProvider _authProvider;

        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;


        public RoleService(AuthenticationStateProvider authProvider, IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _authProvider = authProvider;
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? authType = null;

            // ...

            // Without this, it won't be considered authenticated
            authType = "Token";
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToList();
            var identity = new ClaimsIdentity(claims, authType);
            var state = new AuthenticationState(new ClaimsPrincipal(identity));

            return await Task.FromResult(state);
        }


        public async Task<bool> UserIsInRoleAsync(string role)
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

          

            if (!user.Identity.IsAuthenticated)
            {
                Console.WriteLine("Người dùng chưa được xác thực.");
            }
            else
            {
                Console.WriteLine("Người dùng đã được xác thực.");
            }

            bool checkRole = user.IsInRole(role);
            Console.WriteLine($"Người dùng có thuộc role {role}: {checkRole}");
            return checkRole;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated ?? false;
        }

        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.FindFirst(c => c.Type == "sub")?.Value;
        }
    }

}
