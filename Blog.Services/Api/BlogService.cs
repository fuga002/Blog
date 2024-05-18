using Blog.Data.Repositories;

namespace Blog.Services.Api
{
    public class BlogService
    {
        readonly IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
    }
}
