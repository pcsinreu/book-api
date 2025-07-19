using MediatR;
using book_api.Interface.Input;

namespace book_api.Application.Book.Commands
{
    public record CreateBookCommand(BookInput BookInput) : IRequest<Guid>;
}