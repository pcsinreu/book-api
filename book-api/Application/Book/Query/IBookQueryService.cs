using book_api.Interface.Output;

namespace book_api.Application.Book.Query
{
    public interface IBookQueryService
    {
        Task<List<BookOutput>> GetAllBooks(string? sortBy = null, string? filter = null);

    }
}
