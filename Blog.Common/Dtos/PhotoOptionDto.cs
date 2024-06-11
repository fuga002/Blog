namespace Blog.Common.Dtos;

public class PhotoOptionDto
{
    public int Id { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PublicId { get; set; } // It is for deleting this photo

    public Guid UserId { get; set; }
}