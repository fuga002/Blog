using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Blog.Common.Dtos;
using Blog.Common.Models.Blog;

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
    public async Task<Tuple<BlogDto?, HttpStatusCode>> CreateMyBlog(Guid userId, CreateBlogModel? createBlog)
    {

        var response = await _httpClient.PostAsJsonAsync($"api/users/{userId}/blogs", createBlog);
        if(response.IsSuccessStatusCode)
        {
            var blogDto = JsonSerializer.Deserialize<BlogDto?>(await  response.Content.ReadAsStringAsync());
            return Tuple.Create(blogDto, response.StatusCode);
        }
        return null;
    }
}