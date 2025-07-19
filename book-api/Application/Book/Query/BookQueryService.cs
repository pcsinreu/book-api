using book_api.Application.Book.Query;
using book_api.Infraestructure;
using book_api.Interface.Output;

public class BookQueryService : IBookQueryService
{
    private readonly IBookInfrastructure _bookInfrastructure;

    public BookQueryService(IBookInfrastructure bookInfrastructure)
    {
        _bookInfrastructure = bookInfrastructure;
    }

    public async Task<List<BookOutput>> GetAllBooks(string? sortBy = null, string? filter = null)
    {
        var books = await _bookInfrastructure.GetAllBooksAsync();

        if (!string.IsNullOrEmpty(filter))
        {
            books = books.Where(b =>
                b.Title.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                b.Authors.Any(a => a.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase) || a.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase)) ||
                b.PublishDate.ToString("yyyy-MM-dd").Contains(filter)
            ).ToList();
        }

        books = sortBy switch
        {
            "title" => books.OrderBy(b => b.Title).ToList(),
            "date" => books.OrderBy(b => b.PublishDate).ToList(),
            "authors" => books.OrderBy(b => b.Authors.Count).ToList(),
            _ => books
        };

        return books;
    }
}