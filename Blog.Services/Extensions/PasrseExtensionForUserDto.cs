using Blog.Common.Dtos;
using Blog.Data.Entities;    
namespace Blog.Services.Extensions
{
    public static class PasrseExtensionForUserDto
    {
        public static UserDto ParseModel(this User user)
        {
            var users = new UserDto
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                CreatedAt = user.CreatedAt,
                PhotoUrl = user.PhotoUrl,
            };
            return users;
        }
        public static List<UserDto> ParseModels(this List<User>? users)
        {
            if (users == null || users.Count == 0) throw new ArgumentException("The object is null");
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(user.ParseModel());
            }
            return userDtos;
        }
    }
}
