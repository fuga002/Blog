using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.Blog
{
    public class UpdateBlogModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
