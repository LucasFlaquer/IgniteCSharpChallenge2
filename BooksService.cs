using System;
namespace IgniteCSharpChallenge2;

public class BooksService
{
    private readonly List<Book> books;

    public BooksService()
    {
        books = [];
    }

    public void AddBook(CreateBookDto book)
    {
        Guid uuid = Guid.NewGuid();
        books.Add(new Book
        {
            Id = uuid.ToString(),
            Title = book.Title,
            Author = book.Author,
            Gender = book.Gender,
            Price = book.Price
        });
    }

    public List<Book> getList()
    {
        return books;
    }

    public Book? GetById(string id)
    {
        return books.Find(book => book.Id == id);
        
    }
}