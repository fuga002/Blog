using Blog.Common.Dtos;
using Blog.Data.Entities;
using Blog.Data.Repositories;
using Blog.Services.Extensions;

namespace Blog.Services.Api
{
    public class PostService
    {
        readonly IPostRepository _postRepository;
        readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        // These method is not relevant to user and blog
        public async Task<List<PostDto>> GetAllPosts(Guid userId, int blogId)
        {
            var allPosts = await _postRepository.GetAll();
            return allPosts.ParseModels();
        }


        private async Task CheckPosts(Guid userId, int blogId)
        {
            var user = await _userRepository.GetById(userId);
            var blog = user.Blogs?.FirstOrDefault(blog => blog.Id == blogId);
            var posts = blog?.Posts?.FirstOrDefault(post => post.Id == blogId);
            if (posts is null) throw new Exception("The posts are not relevant!");
        }
    }
}
