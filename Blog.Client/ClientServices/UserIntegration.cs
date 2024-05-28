using System.Net.Http.Json;
using Blog.Common.Dtos;
using Blog.Common.Models.User;

namespace Blog.Client.ClientServices;

public class UserIntegration
{
    private readonly HttpClient _httpClient;

    public UserIntegration(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>?> GetAllUsers() => await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
    public async Task AddUser(CreateUserModel? createUser) => await _httpClient.PostAsJsonAsync("api/users", createUser);
}