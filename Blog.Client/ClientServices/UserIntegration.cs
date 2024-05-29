using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Blog.Common.Dtos;
using Blog.Common.Models.User;
using Blog.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace Blog.Client.ClientServices;

public class UserIntegration
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _storageService;
    private readonly NavigationManager _navigationManager;
    public UserIntegration(HttpClient httpClient, ILocalStorageService storageService, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _storageService = storageService;
        _navigationManager = navigationManager;
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


    public async Task Login(LoginUserModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/login", model);
        var token = "";

        if (response.IsSuccessStatusCode)
        {

            token = await response.Content.ReadAsStringAsync();
        }
        await _storageService.SetItemAsync("jwt-token", token);

        _navigationManager.NavigateTo("allUsers");
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


    private async Task<string?> GetToken()
    {
        return await _storageService.GetItemAsync<string>("jwt-token");
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