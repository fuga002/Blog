using System.Net.Http.Json;
using Blog.Common.Dtos;

namespace Blog.Client.ClientServices;

public class BlogIntegration
{
    private readonly HttpClient _httpClient;

    public BlogIntegration(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BlogDto>?> GetNotRelatedAllBlogs(Guid userId) =>
        await _httpClient.GetFromJsonAsync<List<BlogDto>>($"api/users/{userId}/Blogs/not-related-blogs");

    public async Task<List<BlogDto>?> GetNotRelatedIdBlog(Guid userId, int blogId) =>
        await _httpClient.GetFromJsonAsync<List<BlogDto>>($"api/users/{userId}Blogs/not-related-blogs/{blogId}");
}