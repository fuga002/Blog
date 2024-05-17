namespace Blog.Data.Repositories;

public interface IBlogRepository
{
    public Task<List<Entities.Blog>> GetAll();

    public Task<Entities.Blog> GetById(int id);
    public Task<Entities.Blog> GetByName(string name);
    public Task Create(Entities.Blog blog);
    public Task Update(int id, Entities.Blog newBlog);
    public Task Delete(int id);
}