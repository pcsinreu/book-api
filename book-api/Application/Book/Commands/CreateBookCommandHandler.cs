using book_api.Infraestructure;
using MediatR;

namespace book_api.Application.Book.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookInfrastructure _bookInfrastructure;

        public CreateBookCommandHandler(IBookInfrastructure bookInfrastructure)
        {
            _bookInfrastructure = bookInfrastructure;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookInfrastructure.SaveBookAsync(request.BookInput);
        }
    }
}