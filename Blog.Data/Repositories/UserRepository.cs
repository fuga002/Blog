using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class UserRepository: IUserRepository
{
    private readonly BlogDbContext _context;

    public UserRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>?> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) throw new Exception("User not found");
        return user;
    }

    public async Task<User?> GetByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}