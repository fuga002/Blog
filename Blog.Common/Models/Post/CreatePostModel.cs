using System.ComponentModel.DataAnnotations;
namespace Blog.Common.Models.Post
{
    public class CreatPostModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string RileUrl { get; set; }
        [Required]
        public string AuthorFullName { get; set; }
    }
}
