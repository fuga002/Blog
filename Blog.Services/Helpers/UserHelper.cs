using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Blog.Services.Helpers;

public class UserHelper
{
    private readonly IHttpContextAccessor _accessor;

    public UserHelper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid UserId => Guid.Parse(_accessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public string Username => _accessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name)!;
}