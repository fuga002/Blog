using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class PostRepository : IPostRepository
{
    readonly BlogDbContext _context;

    public PostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task Create(Post post)
    {
        await _context.Posts.AddAsync(post);
    }

    public async Task Delete(int id)
    {
        var postDeleted = await GetById(id);
        _context.Posts.Remove(postDeleted);
    }

    public async Task<List<Post>> GetAll()
    {
        return await _context.Posts.ToListAsync();

    }

    public async Task<Post> GetById(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post is null) return null;
        return post;
    }



    public async Task<Post> GetByTitle(string title)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(post => post.Title == title);
        if (post is null) return null;
        return post;
    }

    public async Task Update(int id, Post newPost)
    {
        if (newPost == null) throw new Exception("The object is null!");
        var postUpdated = await GetById(id);
        postUpdated.Title = newPost.Title;
        postUpdated.FileUrl = newPost.FileUrl;
        postUpdated.Content = newPost.Content;
        postUpdated.CreatedDate = newPost.CreatedDate;
        postUpdated.AuthorFullName = newPost.AuthorFullName;
        postUpdated.Blog = newPost.Blog;

        _context.Posts.Update(postUpdated);
        await _context.SaveChangesAsync();
    }
}