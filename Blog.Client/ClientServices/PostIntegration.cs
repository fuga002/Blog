using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blog.Client.ClientServices.Helpers;
using Blog.Common.Dtos;
using Blog.Common.Models.Post;
using Microsoft.AspNetCore.Components;

namespace Blog.Client.ClientServices;

public class PostIntegration
{
    private readonly HttpClient _httpClient;
    private readonly StorageService _storageService;
    private readonly NavigationManager _navigationManager;

    public PostIntegration(HttpClient httpClient, NavigationManager navigationManager, StorageService storageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _storageService = storageService;
    }

    public async Task<List<PostDto>?> GetAllPosts() => await _httpClient.GetFromJsonAsync<List<PostDto>>("api/posts");

    public async Task<Tuple<PostDto?,string?,bool>> GetPostById(int postId)
    {
        var response = await _httpClient.GetAsync($"api/posts/{postId}");

        if (response.IsSuccessStatusCode)
        {
            var post = await response.Content.ReadFromJsonAsync<PostDto>();
            return new(post, string.Empty, true);
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return new(null, errorMessage, false);
    }

    public async Task<Tuple<PostDto?, string?, bool>> AddPost(Guid userId, int blogId, CreatePostModel model)
    {
        await CheckTokenExist();
        var response = await _httpClient.PostAsJsonAsync($"api/users/{userId}/blogs/{blogId}/Posts", model);

        if (response.IsSuccessStatusCode)
        {
            var postDto = await response.Content.ReadFromJsonAsync<PostDto>();
            return new(postDto,string.Empty, true);
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