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
            var posts = await FilteredPosts(userId, blogId);
            return posts.ParseModels();
        }



        public async Task<PostDto> GetPostById(Guid userId, int blogId, int postId)
        {
            var post = await FilteredPost(userId, blogId, postId);
            return post.ParseToModel();
        }

        
        private async Task<List<Post>> FilteredPosts(Guid userId, int blogId)
        {
            var user = await _userRepository.GetById(userId);
            var blog = user.Blogs?.FirstOrDefault(blog => blog.Id == blogId);
            var filteredPosts = blog?.Posts?.Where(post => post.Id == blogId);
            if (filteredPosts is null) throw new Exception("The object is not found");
            return filteredPosts.ToList();
        }

        private async Task<Post> FilteredPost(Guid userId, int blogId, int postId)
        {
            var user = await _userRepository.GetById(userId);
            var blog = user.Blogs?.FirstOrDefault(blog => blog.Id == blogId);
            var filteredPosts = blog?.Posts?.FirstOrDefault(post => post.Id == postId);
            if (filteredPosts is null) return null;
            return filteredPosts;
        }

    }
}
