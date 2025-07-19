using book_api.Interface.Input;
using book_api.Interface.Output;

namespace book_api.Infraestructure
{

    public interface IBookInfrastructure
    {
        Task<Guid> SaveBookAsync(BookInput bookInput);
        Task<bool> UpdateBookAsync(Guid id, BookInput bookInput);
        Task<List<BookOutput>> GetAllBooksAsync();
    }
}