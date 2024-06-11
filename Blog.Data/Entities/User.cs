using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    
    public virtual  List<PhotoOption>? PhotoOptions { get; set; }

    [Required]

    public string Role { get; set; }

    public virtual List<Blog>? Blogs { get; set; }
}