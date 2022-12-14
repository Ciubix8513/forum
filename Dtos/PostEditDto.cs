namespace Api.Dtos;

public class PostEditDto
{
    public PostEditDto(int id, string contents)
    {
        Id = id;
        Contents = contents;
    }

    public int Id { get; set; }
    public string Contents { get; set; }
}