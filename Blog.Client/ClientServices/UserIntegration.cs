using System.Net;
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

    public async Task<(UserDto? userDto, string errorMessage)> UpdateUserModel(Guid userId, UpdateUserModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{userId}", model);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
            return(userDto,null!);
        }
        else 
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return (null, errorMessage);
        }

        
    } 
}