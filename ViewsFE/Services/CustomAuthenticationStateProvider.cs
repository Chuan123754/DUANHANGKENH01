using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using ViewsFE.IServices;
using ViewsFE.Models;
using ViewsFE.Models.DTO;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAccountService _accountService;
    private readonly HttpClient _httpClient;

    public CustomAuthenticationStateProvider(IAccountService accountService, HttpClient httpClient)
    {
        _accountService = accountService;
        _httpClient = httpClient;
    }

    public async Task LoginAsync(SignInModel model)
    {
        var token = await _accountService.SignInAsync(model);
        var claims = new[] { new Claim(ClaimTypes.Name, model.Email) }; // Thêm các claims khác nếu cần
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        // Lưu token vào header cho các yêu cầu tiếp theo
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Cập nhật trạng thái xác thực
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
    public async Task LogoutAsync()
    {
        await _accountService.SignOutAsync();
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        // Xóa token khỏi header
        _httpClient.DefaultRequestHeaders.Authorization = null;

        // Cập nhật trạng thái xác thực
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var token = _httpClient.DefaultRequestHeaders.Authorization?.Parameter;

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            if (jwtToken != null)
            {
                var claims = jwtToken.Claims;
                user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            }
        }

        return new AuthenticationState(user);
    }
}
