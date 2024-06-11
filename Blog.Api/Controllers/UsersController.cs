using Blog.Common.Models.User;
using Blog.Services.Api;
using Blog.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserHelper _userHelper;

    public UsersController(UserService userService, UserHelper userHelper)
    {
        _userService = userService;
        _userHelper = userHelper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        try
        {
            var user = await _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async  Task<IActionResult> Register([FromBody] CreateUserModel model)
    {
        try
        {
            var user = await _userService.AddUser(model);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserModel model)
    {
        try
        {
            var user = await _userService.Login(model);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(Guid userId,[FromBody]UpdateUserModel model)
    {
        try
        {
            var user = await _userService.UpdateUser(userId, model);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{userId:guid}/add-photo")]
    [Authorize]
    public async Task<IActionResult> AddUserPhoto(Guid userId, IFormFile file)
    {
        try
        {
            var userDto = await _userService.AddUserPhoto(userId, file);
            return Ok(userDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{userId:guid}/delete-photo")]
    [Authorize]
    public async Task<IActionResult> DeleteUserPhoto(Guid userId, string publicId)
    {
        try
        {
            var userDto = await _userService.DeletePhoto(userId, publicId);
            return Ok(userDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var result = await _userService.DeleteUser(userId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

} 