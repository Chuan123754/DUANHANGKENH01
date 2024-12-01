using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Net.Http.Headers;
using ViewsFE.IServices;
using ViewsFE.Models;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAccountService _accountService;
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private bool _hasInitialized;

    public CustomAuthenticationStateProvider(IAccountService accountService, HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _accountService = accountService;
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!_hasInitialized)
        {
            await InitializeAuthenticationState();
        }

        // Nếu chưa có token, trả về trạng thái không xác thực
        var token = await GetTokenFromSessionStorageAsync();

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Tạo ClaimsPrincipal từ token
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "UserFromToken") }; // Giải mã thêm claims nếu cần
        var identity = new ClaimsIdentity(claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task InitializeAuthenticationState()
    {
        if (!_hasInitialized)
        {
            if (!IsPrerendering())
            {
                var token = await GetTokenFromSessionStorageAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                _hasInitialized = true;
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
    }

    public async Task LoginAsync(SignInModel model)
    {
        var token = await _accountService.SignInAsync(model);

        if (!string.IsNullOrEmpty(token))
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var claims = new[] { new Claim(ClaimTypes.Name, model.Email) };
            var identity = new ClaimsIdentity(claims, "jwt");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }
        else
        {
            throw new Exception("Thông tin đăng nhập không chính xác.");
        }
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
