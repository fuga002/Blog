using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities.PostEntity;

public class Post
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public List<string>? Contents { get; set; } = new List<string>();

    public virtual List<PostPhotoOption> PhotoList { get; set; }

    public string? FileUrl { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Required]
    public string AuthorFullName { get; set; }

    public int BlogId { get; set; }
    public virtual Blog? Blog { get; set; }
}