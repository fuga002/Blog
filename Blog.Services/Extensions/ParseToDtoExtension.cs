using Blog.Common.Dtos;
using Blog.Data.Entities;
using System.Runtime.CompilerServices;

namespace Blog.Services.Extensions;

public static class ParseToDtoExtension
{
    public static UserDto ParseToModel(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Username = user.Username,
            CreatedAt = user.CreatedAt,
            PhotoUrl = user.PhotoUrl

            //blogs
        };
    }

    public static List<UserDto> ParseModels(this List<User>? users)
    {
        if (users == null || users.Count == 0) return new List<UserDto>();
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            userDtos.Add(user.ParseToModel());
        }
        return userDtos;
    }
    public static BlogDto ParseToModel(this Blog.Data.Entities.Blog blog)
    {
        return new BlogDto()
        {
            Id = blog.Id,
            Name = blog.Name,
            Description = blog.Description
        };
    }
    public static List<BlogDto> ParseModels(this List<Blog.Data.Entities.Blog>? blogs)
    {
        if (blogs == null || blogs.Count == 0) return new List<BlogDto>();
        var blogDtos = new List<BlogDto>();
        foreach(var blog in blogs)
        {
            blogDtos.Add(blog.ParseToModel());
        }
        return blogDtos;
    }
    public static PostDto ParseModel(this Post post)
    {
        return new PostDto()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            FileUrl = post.FileUrl,
            AuthorFullName = post.AuthorFullName
        };
    }
    public static List<PostDto> ParseModels(this List<Post> posts)
    {
        if (posts == null) return new List<PostDto>();
        var postDtos = new List<PostDto>();
        foreach (var post in posts)
        {
            postDtos.Add(post.ParseModel());
        }
    return postDtos;
    }
}