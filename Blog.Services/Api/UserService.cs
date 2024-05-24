using Blog.Common.Dtos;
using Blog.Common.Models.User;
using Blog.Common.Statics;
using Blog.Data.Entities;
using Blog.Data.Repositories;
using Blog.Services.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Blog.Services.Api;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;

    public UserService(IUserRepository userRepository, JwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAll();
        return users.ParseModels();
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        return user.ParseToModel();
    }

    public async Task<UserDto> AddUser(CreateUserModel model)
    {
        await IsExist(model.Username);

        var user = new User()
        {
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Username = model.Username,
            Role = ConsStrings.UserRole
        };

        var passwordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        user.PasswordHash = passwordHash;
        await _userRepository.Add(user);
        return user.ParseToModel();
    }

    public async Task<string> Login(LoginUserModel model)
    {
        var user = await _userRepository.GetByUsername(model.UserName);
        if (user == null) throw new Exception("Invalid Username");
        
        var result = new  PasswordHasher<User>().VerifyHashedPassword(user,user.PasswordHash,model.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Password failed");
        var token = _jwtTokenService.GenerateToken(user);
        return token;
    }

    public async Task<UserDto> UpdateUser(Guid userId,UpdateUserModel model)
    {
        var user = await _userRepository.GetById(userId);
        if(!string.IsNullOrWhiteSpace(model.Firstname))
            user.Firstname = model.Firstname;
        if(!string.IsNullOrWhiteSpace(model.Lastname))
            user.Lastname = model.Lastname;
        if (!string.IsNullOrWhiteSpace(model.Username))
        {
            await IsExist(model.Username);
            user.Username = model.Username;
        }

        await _userRepository.Update(user);
        return user.ParseToModel();
    }

    public async Task<string> DeleteUser(Guid userId)
    {
        var user = await _userRepository.GetById(userId);
        await _userRepository.Delete(user);
        return "User successfully deleted";
    }


    private async Task IsExist(string username)
    {
        var isExist = await _userRepository.GetByUsername(username);
        if (isExist != null) throw new Exception($"User already exists with this \"{username}\"");
    }
}