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
        public async Task<List<PostDto>> GetAllPosts()
        {
            var allPosts = await _postRepository.GetAll();
            return allPosts.ParseModels();
        }

        // These method is relevant to user and blog
        public async Task<List<PostDto>> GetAllPosts(Guid userId, int blogId)
        {
            var allPosts = await _postRepository.GetAll();
            var filteredPosts = await CheckPosts(userId, blogId, allPosts);
            return filteredPosts.ParseModels();
        }

        private async Task<List<Post>> CheckPosts(Guid userId, int blogId, List<Post>? posts)
        {
            if (posts is null || posts.Count == 0) throw new Exception("The Posts are null!");
            var user = await _userRepository.GetById(userId);
            var blog = user.Blogs?.FirstOrDefault(blog => blog.Id == blogId);
            var relevantPosts = posts.Where(post => post.BlogId == blogId).ToList();
            if (relevantPosts is null) throw new Exception("The posts are not relevant!");
            return relevantPosts;
        }
    }
}
