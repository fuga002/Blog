using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blog.Client.ClientServices.Helpers;

public class UserSettings
{
    private readonly CustomAuthStateProvider _stateProvider;

    public UserSettings(AuthenticationStateProvider stateProvider)
    {
        _stateProvider = (CustomAuthStateProvider)stateProvider;
    }

    public async Task<Guid> GetUserId()
    {
        var state = await _stateProvider.GetAuthenticationStateAsync();

        var userId = state.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        return Guid.Parse(userId);
    }
}