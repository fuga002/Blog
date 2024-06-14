
﻿ namespace Blog.Common.Dtos;

 public class PostDto
 {
     public int Id { get; set; }
     public string Title { get; set; }
     public List<string>? Contents { get; set; }
     public virtual List<PostPhotoOptionDto> PhotoList { get; set; }

    public string? FileUrl { get; set; }
     public DateTime CreatedDate { get; set; }

    public string AuthorFullName { get; set; }
    public int BlogId { get; set; }
}