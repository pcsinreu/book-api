using book_api.Application.Book.Commands;
using book_api.Domain;
using book_api.Infraestructure;
using book_api.Interface.Input;

namespace book_api.Application.Book.Command
{
    public class BookCommandService : IBookCommandService
    {
        private readonly IBookInfrastructure _bookInfrastructure;

        public BookCommandService(IBookInfrastructure bookInfrastructure)
        {
            _bookInfrastructure = bookInfrastructure;
        }

        public async Task<Guid> CreateBookAsync(BookInput bookInput)
        {
            var authors = bookInput.Authors.ConvertAll(a => new Author(a.FirstName, a.LastName));
            var book = new book_api.Domain.Book(Guid.NewGuid(), bookInput.Title, bookInput.PublishDate, authors);
            return await _bookInfrastructure.SaveBookAsync(bookInput);
        }

        public async Task<bool> UpdateBookAsync(Guid id, BookInput bookInput)
        {
            var authors = bookInput.Authors.ConvertAll(a => new Author(a.FirstName, a.LastName));
            var book = new book_api.Domain.Book(id, bookInput.Title, bookInput.PublishDate, authors);
            return await _bookInfrastructure.UpdateBookAsync(id, bookInput);
        }
    }
}