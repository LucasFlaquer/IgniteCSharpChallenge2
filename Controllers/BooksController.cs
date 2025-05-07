using Microsoft.AspNetCore.Mvc;

namespace IgniteCSharpChallenge2.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BooksService bookService;

    public BooksController(BooksService service)
    {
        bookService = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            books = bookService.getList()
        });
    }

    [HttpPost]
    public IActionResult Post([FromBody] BookRequest requestBody)
    {
        if (requestBody == null) { 
            return BadRequest(new { message = "Request body cannot be null." });
        }
        bookService.AddBook(new CreateBookDto
        {
            Author = requestBody.Author,
            Gender = requestBody.Gender,
            Price = requestBody.Price,
            Title = requestBody.Title
        });
        return Created("Create book", new { message = "Book added successfully." });
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] string id)
    {
        var book = bookService.GetById(id);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }
        return Ok(book);
    }
}

public class BookRequest
{
    public required string Title { get; set; } = string.Empty;
    public required string Author { get; set; } = string.Empty;
    public required string Gender { get; set; }
    public required float Price { get; set; }

}

