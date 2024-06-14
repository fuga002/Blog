using System.ComponentModel.DataAnnotations;
using Blog.Data.Entities.PostEntity;

namespace Blog.Data.Entities;

public class Blog
{ [Key]
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string Description { get; set; }

  public string? PhotoUrl { get; set; }
  public string? PhotoPublicId { get; set; }
  
  public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


  public Guid UserId { get; set; }
  public virtual User? User { get; set; }

  public virtual List<Post>? Posts { get; set; } 
}