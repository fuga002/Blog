using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Blog.Client.ClientServices.Helpers;
using Blog.Common.Dtos;
using Blog.Common.Models.Blog;
using Microsoft.AspNetCore.Components;

namespace Blog.Client.ClientServices;

public class BlogIntegration
{
    private readonly HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private readonly StorageService _storageService;
    public BlogIntegration(HttpClient httpClient, NavigationManager navigationManager, StorageService storageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _storageService = storageService;
    }

    public async Task<List<BlogDto>?> GetNotRelatedAllBlogs(Guid userId)
    {
        await AddHeader();
      return  await _httpClient.GetFromJsonAsync<List<BlogDto>>($"api/users/{userId}/Blogs/not-related-blogs");
    }


    public async Task<Tuple<BlogDto?, string?, bool>> CreateMyBlog(Guid userId, CreateBlogModel? createBlog)
    {
        await AddHeader();
        var response = await _httpClient.PostAsJsonAsync($"api/users/{userId}/blogs", createBlog);
        if(response.IsSuccessStatusCode)
        {
            var blogDto = JsonSerializer.Deserialize<BlogDto?>(await  response.Content.ReadAsStringAsync());
            return new(blogDto, string.Empty, true);
        }
       
        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null,errorMessage, false);
    }

    public async Task<Tuple<BlogDto?, string?,bool>> GetNotRelatedBlogById(Guid userId,int blogId)
    {
        await AddHeader();
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
        await AddHeader();
        var response = await _httpClient.GetAsync($"api/users/{userId}/blogs");

        if (response.IsSuccessStatusCode)
        {
            var blogDtos = await response.Content.ReadFromJsonAsync<List<BlogDto>>();
            return new(blogDtos,string.Empty,true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null, errorMessage, false);
    }

    public async Task<Tuple<BlogDto?, string?, bool>> GetMyBlogById(Guid userId, int blogId)
    {
        await AddHeader();
        var response = await _httpClient.GetAsync($"api/users/{userId}/blogs/{blogId}");
        if (response.IsSuccessStatusCode)
        {
            var blogDto = await response.Content.ReadFromJsonAsync<BlogDto>();
            return new(blogDto, string.Empty, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null, errorMessage, false);
    }

    public async Task<Tuple<BlogDto?, string?, bool>> UpdateMyBlog(Guid userId, int blogId, UpdateBlogModel model)
    {
        await AddHeader();
        var response = await _httpClient.PutAsJsonAsync($"api/users/{userId}/blogs/{blogId}", model);
        if (response.IsSuccessStatusCode)
        {
            var blogDto = await response.Content.ReadFromJsonAsync<BlogDto>();
            return new (blogDto,string.Empty, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null, errorMessage, false);
    }

    public async Task<Tuple<string?, bool>> DeleteMyBlog(Guid userId, int blogId)
    {
        await AddHeader();
        var response = await _httpClient.DeleteAsync($"api/users/{userId}/blogs/{blogId}");
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadFromJsonAsync<string>();
            return new (responseString, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(errorMessage, false);
    }

    private async Task AddHeader()
    {
        var token = await _storageService.GetTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}

