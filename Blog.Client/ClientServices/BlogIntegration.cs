using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Blog.Common.Dtos;
using Blog.Common.Models.Blog;
using Microsoft.AspNetCore.Components;

namespace Blog.Client.ClientServices;

public class BlogIntegration
{
    private readonly HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;
    public BlogIntegration(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task<List<BlogDto>?> GetNotRelatedAllBlogs(Guid userId) =>
        await _httpClient.GetFromJsonAsync<List<BlogDto>>($"api/users/{userId}/Blogs/not-related-blogs");
    public async Task<object> CreateMyBlog(Guid userId, CreateBlogModel? createBlog)
    {

        var response = await _httpClient.PostAsJsonAsync($"api/users/{userId}/blogs", createBlog);
        if(response.IsSuccessStatusCode)
        {
            var blogDto = JsonSerializer.Deserialize<BlogDto?>(await  response.Content.ReadAsStringAsync());
            return blogDto;
        }
        return response.StatusCode;
    }

    public async Task<Tuple<BlogDto?, string?,bool>> GetNotRelatedBlogById(Guid userId,int blogId)
    {
        var response = await _httpClient.GetAsync($"api/users/{userId}/Blogs/not-related-blogs/{blogId}");

        if (response.IsSuccessStatusCode)
        {
            var blogDto = await response.Content.ReadFromJsonAsync<BlogDto>();
            return new(blogDto,string.Empty, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();

        return new(null, errorMessage, false);
    }

    public async Task<Tuple<List<BlogDto>?, string?, bool>> GetMyBlogs(Guid userId)
    {
        var response = await _httpClient.GetAsync($"api/users/{userId}/blogs");

        if (response.IsSuccessStatusCode)
        {
            var blogDtos = await response.Content.ReadFromJsonAsync<List<BlogDto>>();
            return new(blogDtos,string.Empty,true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null, errorMessage, false);
    }


    private async Task<string?> GetToken()
    {
        return await _localStorageService.GetItemAsync<string>("jwt-token");
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