using System.Net;
using System.Net.Http.Json;
using Blog.Common.Dtos;

namespace Blog.Client.ClientServices;

public class PostIntegration
{
    private readonly HttpClient _httpClient;

    public PostIntegration(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

    public async Task<Tuple<List<PostDto>, string, bool>> GetMyAllPosts(Guid userId,int blogId)
    {
        
        var response = await _httpClient.GetAsync($"api/users/{userId}/blogs{blogId}/posts");
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<List<PostDto>>();
            return new(content, string.Empty, true);
        }

        var errormessage = response.Content.ReadAsStringAsync();
        return new(null, errormessage.ToString()!, false);
        
    }
}