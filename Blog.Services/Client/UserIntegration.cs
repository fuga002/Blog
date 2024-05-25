using System.Net.Http.Json;
using Blog.Common.Dtos;

namespace Blog.Services.Client;

public class UserIntegration
{
    private readonly HttpClient _httpClient;

    public UserIntegration(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>?> GetAllUsers() => await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
}