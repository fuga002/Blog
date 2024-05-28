using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blog.Common.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blog.Client.AuthActions;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public AuthService(HttpClient httpClient, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorageService = localStorageService;
    }

    public async Task<bool> Login(LoginUserModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", model);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<string>();
            var token = result;

            await _localStorageService.SetItemAsync("token", token);



            return true;
        }

        return false;
    }
}