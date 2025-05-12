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
        if (requestBody == null)
        {
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

    [HttpPut("{id}")]
    public IActionResult Update(UpdateBookRequest request)
    {
        if (request.Body == null)
        {
            return BadRequest(new { message = "Request body cannot be null." });
        }
        var book = bookService.GetById(request.Id);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }
        UpdateBodyRequest requestBody = request.Body;
        bookService.Update(new UpdateBookDto
        {
            Id = book.Id,
            Title = requestBody.Title ?? book.Title,
            Author = requestBody.Author ?? book.Author,
            Gender = requestBody.Gender ?? book.Gender,
            Price = requestBody.Price ?? book?.Price
        });
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById([FromRoute] string id) 
    {
        bookService.Delete(id);
        return NoContent();
    }
}

public class BookRequest
{
    public required string Title { get; set; } = string.Empty;
    public required string Author { get; set; } = string.Empty;
    public required string Gender { get; set; }
    public required decimal Price { get; set; }
}

public class UpdateBodyRequest
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Gender { get; set; }
    public decimal Price { get; set; } = decimal.Zero;
}

public class UpdateBookRequest
{
    [FromRoute]
    public required string Id { get; set; }

    [FromBody]
    public required UpdateBodyRequest Body { get; set; }

}