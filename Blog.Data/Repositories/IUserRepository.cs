using Blog.Data.Entities;

namespace Blog.Data.Repositories;

public interface IUserRepository
{
    public Task<List<User>> GetAll();

    public Task<User> GetById(Guid id);
    public Task<User> GetByUserName(string name);
    public Task Create(User user);
    public Task Update(Guid id, User user);
    public Task Delete(Guid id);

}