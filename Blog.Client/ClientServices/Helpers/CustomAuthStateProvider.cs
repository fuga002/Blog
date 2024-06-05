using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blog.Client.ClientServices.Helpers;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly StorageService _storageService;

    public CustomAuthStateProvider(StorageService storageService)
    {
        _storageService = storageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsPrincipal = await SetClaims();
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }


    public async Task UpdateState()
    {
        var claimsPrincipal = await SetClaims();

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

    }



    private async Task<ClaimsPrincipal> SetClaims()
    {
        var (userId, username, check) = await DeserializeJwtToken();
        var claimsPrincipal = new ClaimsPrincipal();
        if (check)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, userId!),
                    new Claim(ClaimTypes.Name, username!)
            },authenticationType: "JwtAuth"));
        }
        return claimsPrincipal;
    }


    private async Task<Tuple<string?, string?, bool>> DeserializeJwtToken()
    {
        var token = await _storageService.GetTokenAsync();
        if (token == null) return new(string.Empty, string.Empty, false);

        var security = new JwtSecurityTokenHandler();
        var tokenParams = security.ReadJwtToken(token);

        var userId = tokenParams.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var username = tokenParams.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;

        return new(userId, username, true);
    }

}