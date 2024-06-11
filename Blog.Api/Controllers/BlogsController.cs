using Blog.Common.Models.Blog;
using Blog.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/users/{userId:guid}/[controller]")]
[ApiController]
[Authorize]
public class BlogsController : ControllerBase
{
    private readonly BlogService _blogService;
    public BlogsController(BlogService blogService)
    {
        _blogService = blogService;
    }

    //These apis for not related blogs

    [HttpGet("not-related-blogs")]
    public async Task<IActionResult> GetAllNorRelatedBlogs(Guid userId)
    {
        try
        {
            var blogs = await _blogService.GetAllNorRelatedBlogs(userId);
            return Ok(blogs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("not-related-blogs/{blogId:int}")]
    public async Task<IActionResult> GetNotRelatedBlogById(Guid userId, int blogId)
    {
        try
        {
            var blog = await _blogService.GetNotRelatedBlogById(userId, blogId);
            return Ok(blog);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Related blogs
    [HttpGet]
    public async Task<IActionResult> GetAllUserBlogs(Guid userId)
    {
        try
        {
            var blogs = await _blogService.GetAllUserBlogs(userId);
            return Ok(blogs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{blogId:int}")]
    public async Task<IActionResult> GetUserBlogById(Guid userId, int blogId)
    {
        try
        {
            var blog = await _blogService.GetRelatedBlogById(userId, blogId);
            return Ok(blog);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUserBlog(Guid userId, [FromBody] CreateBlogModel model)
    {
        try
        {
            var blog = await _blogService.AddBlog(userId, model);
            return Ok(blog);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{blogId:int}")]
    public async Task<IActionResult> UpdateUserBlog(Guid userId, int blogId,[FromBody] UpdateBlogModel model)
    {
        try
        {
            var blog = await _blogService.UpdateBlog(userId, blogId, model);
            return Ok(blog);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{blogId:int}")]
    public async Task<IActionResult> DeleteUserBlog(Guid userId, int blogId)
    {
        try
        {
            var result = await _blogService.DeleteBlog(userId,blogId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




}