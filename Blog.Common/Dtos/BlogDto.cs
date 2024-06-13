namespace Blog.Common.Dtos;

public class BlogDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public string? PhotoUrl { get; set; }
    public string? PhotoPublicId { get; set; }

    public List<PostDto>? Posts { get; set; }
}