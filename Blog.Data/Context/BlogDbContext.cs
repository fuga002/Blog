using Blog.Data.Entities;
using Blog.Data.Entities.PostEntity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context;

public class BlogDbContext:DbContext
{

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Entities.Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}