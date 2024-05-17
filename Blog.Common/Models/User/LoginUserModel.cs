using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User;

public class LoginUserModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}