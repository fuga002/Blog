namespace Blog.Data.Entities.PostEntity;

public class PostPhotoOption
{
    public int Id { get; set; }
    public string? PhotoUrl { get; set; }
    public string? PublicId { get; set; } // It is for deleting this photo

    public int PostId { get; set; }
    public virtual Post? Post { get; set; }
}