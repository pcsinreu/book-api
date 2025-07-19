using MediatR;
using book_api.Interface.Output;
using System.Collections.Generic;

namespace book_api.Application.Book.Query
{
    public record GetBooksQuery(string? SortBy, string? Filter) : IRequest<List<BookOutput>>;
}