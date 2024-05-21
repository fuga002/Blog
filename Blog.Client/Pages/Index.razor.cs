using Blog.Common.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Blog.Client.Pages;

public partial class Index :ComponentBase
{
    private readonly HttpClient _client = new HttpClient();

    private List<UserDto>? _users = new List<UserDto>();

    protected override async Task OnInitializedAsync()
    {
        _users = await _client.GetFromJsonAsync<List<UserDto>>("https://localhost:7105/api/Users");
    }
}