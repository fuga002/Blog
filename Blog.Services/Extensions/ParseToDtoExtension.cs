using Blog.Common.Dtos;
using Blog.Data.Entities;
using Mapster;

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
            PhotoOptions = user.PhotoOptions.ParseModels()!,
            Blogs = user.Blogs.ParseModels()
        };
    }

    public static PhotoOptionDto ParseModel(this PhotoOption option)
    {
        PhotoOptionDto model = option.Adapt<PhotoOptionDto>();
        return model;
    }

    public static List<PhotoOptionDto> ParseModels(this List<PhotoOption>? options)
    {
        var dtos = new List<PhotoOptionDto>();
        if (options == null || options.Count == 0) return new List<PhotoOptionDto>();
        foreach (var option in options)
        {
            dtos.Add(option.ParseModel());
        }

        return dtos;
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
        BlogDto blogDto = blog.Adapt<BlogDto>();
        return blogDto;
    }

    public static List<BlogDto> ParseModels(this List<Blog.Data.Entities.Blog>? blogs)
    {
        var dtos = new List<BlogDto>();
        if (blogs == null || blogs.Count == 0) return new List<BlogDto>();
        foreach (var blog in blogs)
        {
            dtos.Add(blog.ParseToModel());
        }

        return dtos;
    }


    public static PostDto ParseToModel(this Post post )
    {
        PostDto postDto = post.Adapt<PostDto>();
        return postDto;
    }

    public static List<PostDto> ParseModels(this List<Post>? posts)
    {
        var dtos = new List<PostDto>();
        if (posts == null || posts.Count == 0) return new List<PostDto>();
        foreach(var post in posts)
        {
            dtos.Add(post.ParseToModel());
        }
        return dtos;
    }

}