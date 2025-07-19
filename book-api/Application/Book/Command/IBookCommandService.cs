using book_api.Interface.Input;

namespace book_api.Application.Book.Commands
{
    public interface IBookCommandService
    {
        Task<Guid> CreateBookAsync(BookInput bookInput);
        Task<bool> UpdateBookAsync(Guid id, BookInput bookInput);
    }
}
