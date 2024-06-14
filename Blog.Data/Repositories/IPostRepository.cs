using Blog.Data.Entities.PostEntity;

namespace Blog.Data.Repositories;

public interface IPostRepository
{

    public Task<List<Post>?> GetAll();
    public Task<Post> GetById(int id);
    public Task Add(Post post);
    public Task Update(Post post);
    public Task DeleteById(Post post);
}