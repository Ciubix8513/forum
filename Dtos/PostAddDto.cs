namespace Api.Dtos;

public class PostAddDto
{
    public PostAddDto(string content, int parentPostId)
    {
        Content = content;
        ParentPostId = parentPostId;
    }

    public string Content { get; set; }
    public int ParentPostId { get; set; }
}