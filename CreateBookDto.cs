namespace IgniteCSharpChallenge2;

public class CreateBookDto
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Gender { get; set; }
    public required float Price { get; set; }
}
