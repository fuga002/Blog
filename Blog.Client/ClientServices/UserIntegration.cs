using System.Net.Http.Json;
using System.Text.Json;
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

    public async Task<object> CreateUser(CreateUserModel model)
    {
        var request = await _httpClient.PostAsJsonAsync("api/users", model);
        if(request.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var userDto =  JsonSerializer.Deserialize<UserDto>( await request.Content.ReadAsStringAsync());
            return userDto;
        }
        return request.StatusCode;
    }

}