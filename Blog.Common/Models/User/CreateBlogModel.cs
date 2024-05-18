using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User
{
    public class CreateBlogModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
