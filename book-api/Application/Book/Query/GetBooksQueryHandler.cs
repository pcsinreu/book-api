using MediatR;
using book_api.Interface.Output;
using book_api.Infraestructure;

namespace book_api.Application.Book.Query
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookOutput>>
    {
        private readonly IBookInfrastructure _bookInfrastructure;

        public GetBooksQueryHandler(IBookInfrastructure bookInfrastructure)
        {
            _bookInfrastructure = bookInfrastructure;
        }

        public async Task<List<BookOutput>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookInfrastructure.GetAllBooksAsync();
            return books;
        }
    }
}