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
        public async Task<PostDto> GetPostById(Guid Userid, int Blogid, int Postid)
        {
            var userId = await _userRepository.GetById(Userid);
            var blogId = userId?.Blogs?.FirstOrDefault(p => p.Id == Blogid);
            var postId = blogId?.Posts?.FirstOrDefault(p => p.Id == Postid);
            return postId.ParseToModel();
        }



    }
}
