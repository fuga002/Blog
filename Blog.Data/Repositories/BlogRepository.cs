using Blog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class BlogRepository: IBlogRepository
{
    private readonly BlogDbContext _dbContext;

    public BlogRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Entities.Blog>?> GetAll()
    {
        var blogs = await _dbContext.Blogs.ToListAsync();
        return blogs;
    }

    public async Task<Entities.Blog> GetById(int id)
    {
        var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        if (blog is null) throw new Exception("Blog Not Found");
        return blog;
    }

    public async Task<Entities.Blog?> GetByName(string name)
    {
        var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Name == name);
        return blog;
    }

    public async Task Add(Entities.Blog blog)
    {
        _dbContext.Blogs.Add(blog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Entities.Blog blog)
    {
        _dbContext.Blogs.Update(blog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Entities.Blog blog)
    {
        _dbContext.Blogs.Remove(blog);
        await _dbContext.SaveChangesAsync();
    }
}