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

    public void Update(UpdateBookDto bookToUpdate) 
    {
        int index = books.FindIndex(book => book.Id == bookToUpdate.Id);
        if(index == -1)
        {
            throw new Exception("Book not found");
        }
        books[index].Title = bookToUpdate.Title;
        books[index].Author = bookToUpdate.Author;
        books[index].Gender = bookToUpdate.Gender;
        books[index].Price = bookToUpdate.Price;
    }

    public void Delete(string id)
    {
        Book? book = GetById(id);
        if(book == null)
        {
            throw new ArgumentNullException("id");
        }
        books.Remove(book);
    }
}