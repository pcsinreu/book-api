using book_api.Infraestructure;
using MediatR;

namespace book_api.Application.Book.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookInfrastructure _bookInfrastructure;

        public UpdateBookCommandHandler(IBookInfrastructure bookInfrastructure)
        {
            _bookInfrastructure = bookInfrastructure;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookInfrastructure.UpdateBookAsync(request.Id, request.BookInput);
        }
    }
}