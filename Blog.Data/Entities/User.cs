using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public  string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? PhotoUrl { get; set; }

    public List<Blog>? Blogs { get; set; }
}