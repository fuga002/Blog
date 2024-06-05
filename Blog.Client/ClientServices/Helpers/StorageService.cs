using Blazored.LocalStorage;

namespace Blog.Client.ClientServices.Helpers;

public class StorageService
{
    private readonly ILocalStorageService _localStorageService;

    public StorageService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    private const string TokenName = "jwt-token";

    public async Task SetTokenAsync(string token) => await _localStorageService.SetItemAsync(TokenName, token);

    public async Task<string?> GetTokenAsync() => await _localStorageService.GetItemAsync<string>(TokenName);

    public async Task RemoveTokenAsync() => await _localStorageService.RemoveItemAsync(TokenName);
}