using System.Diagnostics.Metrics;
using Blog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class BlogRepository: IBlogRepository
{
    readonly BlogDbContext _context;
    public BlogRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task Create(Entities.Blog blog)
    {
        await _context.Blogs.AddAsync(blog);
    }

    public async Task Delete(int id)
    {
        var blogDeleted = await GetById(id);
        _context.Blogs.Remove(blogDeleted);
    }

    public async Task<List<Entities.Blog>> GetAll()
    {
        return await _context.Blogs.ToListAsync();

    }

    public async Task<Entities.Blog> GetById(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog is null) return null;
        return blog;
    }



    public async Task<Entities.Blog> GetByName(string blogName)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(blog => blog.Name == blogName);
        if (blog is null) return null;
        return blog;
    }

    public async Task Update(int id, Entities.Blog newBlog)
    {
        if (newBlog == null) throw new Exception("The object is null!");
        var blogUpdated = await GetById(id);
        blogUpdated.Name = newBlog.Name;
        blogUpdated.Description = newBlog.Description;
        blogUpdated.CreatedDate = newBlog.CreatedDate;
        blogUpdated.User = newBlog.User;
        blogUpdated.Posts = newBlog.Posts;

        _context.Blogs.Update(blogUpdated);
        await _context.SaveChangesAsync();
    }
}