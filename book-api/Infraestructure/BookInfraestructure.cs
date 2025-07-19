using book_api.Infraestructure;
using book_api.Interface.Input;
using book_api.Interface.Output;

public class BookInfrastructure : IBookInfrastructure
{
    private static readonly List<BookOutput> Books = new();

    public Task<Guid> SaveBookAsync(BookInput bookInput)
    {
        var id = Guid.NewGuid();
        var authors = bookInput.Authors.Select(a => new AuthorOutput(a.FirstName, a.LastName)).ToList();
        var book = new BookOutput(id, bookInput.Title, bookInput.PublishDate, authors);
        Books.Add(book);
        return Task.FromResult(id);
    }

    public Task<bool> UpdateBookAsync(Guid id, BookInput bookInput)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return Task.FromResult(false);

        Books.Remove(book);
        var authors = bookInput.Authors.Select(a => new AuthorOutput(a.FirstName, a.LastName)).ToList();
        var updatedBook = new BookOutput(id, bookInput.Title, bookInput.PublishDate, authors);
        Books.Add(updatedBook);
        return Task.FromResult(true);
    }

    public Task<List<BookOutput>> GetAllBooksAsync()
    {
        return Task.FromResult(Books.ToList());
    }
}