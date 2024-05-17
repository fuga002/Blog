using Blog.Data.Entities;

namespace Blog.Data.Repositories;

public interface IPostRepository
{
    public Task<List<Post>> GetAll();

    public Task<Post> GetById(int id);
    public Task<Post> GetByTitle(string title);
    public Task Create(Post post);
    public Task Update(int id, Post post);
    public Task Delete(int id);
}