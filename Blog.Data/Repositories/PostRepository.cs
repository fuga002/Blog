using Blog.Data.Context;
using Blog.Data.Entities.PostEntity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class PostRepository: IPostRepository
{
    private readonly BlogDbContext _dbContext;

    public PostRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Post>?> GetAll() => await _dbContext.Posts.ToListAsync();

    public async Task<Post> GetById(int id)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post is null) throw new Exception("Post Not Found");
        return post;
    }

    public async Task Add(Post post)
    {
        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Post post)
    {
        _dbContext.Posts.Update(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(Post post)
    {
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}