using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Blog.Data.Repositories;

public class UserRepository: IUserRepository
{
    readonly BlogDbContext _context;

    public UserRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task Delete(Guid id)
    {
        var userDeleted = await GetById(id);
        _context.Users.Remove(userDeleted);
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();

    }

    public async Task<User> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) return null;
        return user;
    }



    public async Task<User> GetByUserName(string userName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == userName);
        if (user is null) return null;
        return user;
    }

    public async Task Update(Guid id, User newUser)
    {
        if (newUser == null) throw new Exception("The object is null!");
        var userUpdated = await GetById(id);
        userUpdated.Firstname = newUser.Firstname;
        userUpdated.Lastname = newUser.Lastname;
        userUpdated.Username = newUser.Username;
        userUpdated.CreatedAt = newUser.CreatedAt;
        userUpdated.PhotoUrl = newUser.PhotoUrl;
        userUpdated.Blogs = newUser.Blogs;
        _context.Users.Update(userUpdated);
        await _context.SaveChangesAsync();
    }
}