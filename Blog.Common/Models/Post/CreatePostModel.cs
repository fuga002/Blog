using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.Post
{
    public class CreatePostModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string FileUrl { get; set; }
        [Required]
        public string AuthorFullName { get; set; }
    }
}
