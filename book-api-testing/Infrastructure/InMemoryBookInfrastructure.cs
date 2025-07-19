using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using book_api.Interface.Input;
using book_api.Infraestructure;
using book_api.Interface.Output;

public class InMemoryBookInfrastructure : IBookInfrastructure
{
    private readonly Dictionary<Guid, BookInput> _books = new();

    public Task<Guid> SaveBookAsync(BookInput bookInput)
    {
        var id = Guid.NewGuid();
        _books[id] = bookInput;
        return Task.FromResult(id);
    }

    public Task<bool> UpdateBookAsync(Guid id, BookInput bookInput)
    {
        if (_books.ContainsKey(id))
        {
            _books[id] = bookInput;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<List<BookOutput>> GetAllBooksAsync()
    {
        throw new NotImplementedException();
    }
}