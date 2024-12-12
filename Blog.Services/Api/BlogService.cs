using Blog.Common.Dtos;
using Blog.Common.Models.Blog;
using Blog.Data.Entities;
using Blog.Data.Repositories;
using Blog.Services.Extensions;
using Microsoft.AspNetCore.Http;

namespace Blog.Services.Api;

public class BlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUserRepository _userRepository;
    private readonly PhotoService _photoService;
    public BlogService(IBlogRepository blogRepository, IUserRepository userRepository, PhotoService photoService)
    {
        _blogRepository = blogRepository;
        _userRepository = userRepository;
        _photoService = photoService;
    }

    //These methods for not related blogs
    public async Task<List<BlogDto>> GetAllNorRelatedBlogs(Guid userId)
    {
        await CheckUser(userId);
        var blogs = await _blogRepository.GetAll();
        return blogs.ParseModels();
    }

    public async Task<BlogDto> GetNotRelatedBlogById(Guid userId,int blogId)
    {
        await CheckUser(userId);
        var blog = await _blogRepository.GetById(blogId);
        return blog.ParseToModel();
    }

    //These methods for  related to user,  blogs
    public async Task<List<BlogDto>> GetAllUserBlogs(Guid userId)
    {
        await CheckUser(userId);
        var blogs = await _blogRepository.GetAll();
        var relatedBlogs = blogs?.Where(b => b.UserId == userId).ToList();
        return relatedBlogs.ParseModels();
    }

    public async Task<BlogDto> GetRelatedBlogById(Guid userId, int blogId)
    {
        var blog = await GetBlogById(userId, blogId);
        return blog.ParseToModel();
    }

    public async Task<BlogDto> AddBlog(Guid userId, CreateBlogModel model)
    {
        await CheckUser(userId);

        await IsExist(model.Name);

        Data.Entities.Blog blog = new()
        {
            Name = model.Name,
            Description = model.Description,
            UserId = userId
        };
        await _blogRepository.Add(blog);
        return blog.ParseToModel();
    }

    public async Task<BlogDto> UpdateBlog(Guid userId,int blogId, UpdateBlogModel model)
    {

        var blog = await GetBlogById(userId, blogId);

        var check = false;

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            await IsExist(model.Name);
            blog.Name = model.Name;
            check =true;
        }

        if (!string.IsNullOrEmpty(model.Description))
        {
            blog.Description = model.Description;
            check = true;
        }
        
        if(check) await _blogRepository.Update(blog);
        return blog.ParseToModel();
    }

    public async Task<BlogDto> AddBlogPhoto(Guid userId, int blogId, IFormFile file)
    {
        var blog = await GetBlogById(userId, blogId);

        var uploadResult = await _photoService.AddPhoto(file);

        blog.PhotoUrl = uploadResult.SecureUrl.AbsoluteUri;
        blog.PhotoPublicId = uploadResult.PublicId;

        await _blogRepository.Update(blog);
        return blog.ParseToModel();
    }

    public async Task<BlogDto> ChangeBlogPhoto(Guid userId, int blogId, IFormFile? file)
    {
        var blog = await GetBlogById(userId, blogId);

        if (file != null)
        {
            if (!string.IsNullOrWhiteSpace(blog.PhotoPublicId))
            {
                await _photoService.DeletePhoto(blog.PhotoPublicId);
            }

            var uploadImgResult = await _photoService.AddPhoto(file);

            blog.PhotoUrl = uploadImgResult.SecureUrl.AbsoluteUri;
            blog.PhotoPublicId = uploadImgResult.PublicId;
            await _blogRepository.Update(blog);
        }
        else
        {
            throw new Exception("Please, input photo :)");
        }

        return blog.ParseToModel();
    }

    public async Task<string> DeleteBlog(Guid userId, int blogId)
    {
        var blog = await GetBlogById(userId, blogId);

        await _blogRepository.Delete(blog);
        return "Deleted successfully";
    }


    private async Task<User> CheckUser(Guid userId)
    {
        var user = await _userRepository.GetById(userId);
        return user;
    }

    private async Task IsExist(string name)
    {
        var blog = await _blogRepository.GetByName(name.ToLower());
        if (blog is not null) throw new Exception($"This name \"{name}\" is already exist ");
    }

    private async Task<Data.Entities.Blog> GetBlogById(Guid userId, int blogId)
    {
        var user = await CheckUser(userId);
        var blog = user.Blogs?.FirstOrDefault(b => b.Id == blogId);
        if (blog is null) throw new Exception($"Invalid blogId \"{blogId}\"");
        return blog;
    }


}