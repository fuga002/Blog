using Blog.Common.Models.Post;
using Blog.Services.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/users/{userId:guid}/blogs/{blogId:int}/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly PostService _postService;

    public PostsController(PostService postService)
    {
        _postService = postService;
    }


    [HttpGet("/api/posts")]
    public async Task<IActionResult> GetAllPosts()
    {
        try
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("/api/posts/{postId:int}")]
    [Authorize]
    public async Task<IActionResult> GetPostById(int postId)
    {
        try
        {
            var post = await _postService.GetPostById(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserPosts(Guid userId, int blogId)
    {
        try
        {
            var posts = await _postService.GetAllPosts(userId, blogId);
            return Ok(posts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{postId:int}")]
    public async Task<IActionResult> GetUserPostById(Guid userId, int blogId, int postId)
    {
        try
        {
            var post = await _postService.GetPostById(userId, blogId, postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUserPost(Guid userId, int blogId, [FromBody] CreatePostModel model)
    {
        try
        {
            var post = await _postService.AddPost(userId, blogId, model);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{postId:int}")]
    public async Task<IActionResult> UpdatePost(Guid userId, int blogId, int postId, [FromBody] UpdatePostModel model)
    {
        try
        {
            var post = await _postService.UpdatePost(userId,blogId,postId,model);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{postId:int}")]
    public async Task<IActionResult> DeletePost(Guid userId, int blogId, int postId)
    {
        try
        {
            var result = await _postService.DeletePost(userId, blogId, postId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

} 