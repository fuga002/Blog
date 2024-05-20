using Blog.Common.Dtos;
using Blog.Data.Repositories;
using Blog.Services.Extensions;

namespace Blog.Services.Api
{
    public class BlogService
    {
        readonly IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<List<BlogDto>> GetAllBlogs()
        {
            var blogs = await _blogRepository.GetAll();
            return blogs.ParseModels();
        }
        public async Task<BlogDto> GetBlogById(int id)
        {
            var blog =  await _blogRepository.GetById(id);
            return blog.ParseToModel();
        }
    }
}
