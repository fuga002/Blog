namespace Blog.Common.Dtos;

public class BlogDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set;} = DateTime.UtcNow;
    public UserDto? User { get; set; }
    public List<PostDto>? Posts { get; set; }
}