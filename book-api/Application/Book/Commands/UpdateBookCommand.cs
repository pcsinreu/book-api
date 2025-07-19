using MediatR;
using book_api.Interface.Input;

namespace book_api.Application.Book.Commands
{
    public record UpdateBookCommand(Guid Id, BookInput BookInput) : IRequest<bool>;
}