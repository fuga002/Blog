using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blog.Client.ClientServices.Helpers;
using Blog.Common.Dtos;
using Blog.Common.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace Blog.Client.ClientServices;

public class UserIntegration
{
    private readonly HttpClient _httpClient;
    private readonly StorageService _storageService;
    private readonly NavigationManager _navigationManager;
    public UserIntegration(HttpClient httpClient, NavigationManager navigationManager, StorageService storageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _storageService = storageService;
    }

    public async Task<List<UserDto>?> GetAllUsers()
    {
        await CheckTokenExist();

        return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        await CheckTokenExist();

       return await _httpClient.GetFromJsonAsync<UserDto>($"api/Users/{userId}");
    }


    public async Task<object> CreateUser(CreateUserModel model)
    {
        var request = await _httpClient.PostAsJsonAsync("api/users", model);
        if (request.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var userDto = JsonSerializer.Deserialize<UserDto>(await request.Content.ReadAsStringAsync());
            return userDto;
        }
        return request.StatusCode;
    }


    public async Task<Tuple<string?,string?,bool>> Login(LoginUserModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/login", model);
        var token = "";

        if (response.IsSuccessStatusCode)
        {
            token = await response.Content.ReadAsStringAsync();
            await _storageService.SetTokenAsync(token);
            return new(token, string.Empty, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();

        return new(string.Empty, errorMessage, false);
    }

    public async Task<(UserDto? userDto, string errorMessage)> UpdateUserModel(Guid userId, UpdateUserModel model)
    {
        await CheckTokenExist();

        var response = await _httpClient.PutAsJsonAsync($"api/users/{userId}", model);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
            return (userDto, null!);
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return (null, errorMessage);
        }


    }


    public async Task DeleteUser(Guid userId)
    {
        await CheckTokenExist();
        await _httpClient.DeleteAsync($"api/users/{userId}");
        _navigationManager.NavigateTo("allUsers");
    }


    public async Task<Tuple<UserDto?, string?, bool>> UploadImg(Guid userId, MultipartFormDataContent content)
    {
        await CheckTokenExist();

        var response = await _httpClient.PutAsJsonAsync($"api/users/{userId}/add-photo", content);

        if (response.IsSuccessStatusCode)
        {
            var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
            return new(userDto, String.Empty, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null, errorMessage, false);

    }

    private async Task<string?> GetToken()
    {
        return await _storageService.GetTokenAsync();
    }

    private async Task CheckTokenExist()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            _navigationManager.NavigateTo("/login");
        }
    }
}