using book_api.Application.Book.Commands;
using book_api.Infraestructure;
using book_api.Interface.Input;

public class BookCommandService : IBookCommandService
{
    private readonly IBookInfrastructure _bookInfrastructure;

    public BookCommandService(IBookInfrastructure bookInfrastructure)
    {
        _bookInfrastructure = bookInfrastructure;
    }

    public Task<Guid> CreateBookAsync(BookInput bookInput)
    {
        return _bookInfrastructure.SaveBookAsync(bookInput);
    }

    public Task<bool> UpdateBookAsync(Guid id, BookInput bookInput)
    {
        return _bookInfrastructure.UpdateBookAsync(id, bookInput);
    }
}