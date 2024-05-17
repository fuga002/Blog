using Blog.Common.Dtos;
using Blog.Data.Entities;

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
}