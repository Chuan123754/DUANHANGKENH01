using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Net.Http.Headers;
using ViewsFE.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAccountService _accountService;
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public CustomAuthenticationStateProvider(IAccountService accountService, HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _accountService = accountService;
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }
    public async Task<string> GetCurrentUserId()
    {
        var authState = await GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            // Giả sử bạn đã lưu ID người dùng trong claims  
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        return null; // Trả về null nếu không tìm thấy  
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await GetTokenFromSessionStorageAsync();

        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        foreach (var kvp in keyValuePairs)
        {
            claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
        }

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
    public async Task InitializeAuthenticationState()
    {
        var token = await GetTokenFromSessionStorageAsync();
        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
    public async Task LoginAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);

        // Thiết lập header Authorization cho HttpClient  
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Giải mã token để lấy claims  
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToList();

        var identity = new ClaimsIdentity(claims, "jwt");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public async Task LogoutAsync()
    {
        await _accountService.SignOutAsync();
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
    }

    private async Task<string> GetTokenFromSessionStorageAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
        }
        catch
        {
            return string.Empty;
        }
    }

    private bool IsPrerendering()
    {
        return _jsRuntime is not IJSInProcessRuntime;
    }
}