using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User;

public class UpdateUserModel
{
    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]

    public string Username { get; set; }
}