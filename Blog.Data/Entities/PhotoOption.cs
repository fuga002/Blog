namespace Blog.Data.Entities;

public class PhotoOption
{
    public int Id { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PublicId { get; set;} // It is for deleting this photo

    public Guid UserId { get; set; } 
    public virtual User? User { get; set; }
}