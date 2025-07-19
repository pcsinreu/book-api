using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using book_api.Interface.Input;
using book_api.Application.Book.Commands;
using book_api.Application.Book.Command;

namespace book_api_testing.Application  
{
    public class BookCommandServiceTests
    {
        private readonly IBookCommandService _service;

        public BookCommandServiceTests()
        {
            _service = new BookCommandService(new InMemoryBookInfrastructure());
        }

        [Fact]
        public async Task CreateBookAsync_ReturnsValidGuid()
        {
            var bookInput = new BookInput("Test Book", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            var result = await _service.CreateBookAsync(bookInput);
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task UpdateBookAsync_ReturnsTrue_WhenBookExists()
        {
            var bookInput = new BookInput("Test Book", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            var id = await _service.CreateBookAsync(bookInput);
            var updatedInput = new BookInput("Updated Book", DateTime.Now, new List<AuthorInput> { new AuthorInput("Jane", "Smith") });
            var result = await _service.UpdateBookAsync(id, updatedInput);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateBookAsync_ReturnsFalse_WhenBookDoesNotExist()
        {
            var updatedInput = new BookInput("Updated Book", DateTime.Now, new List<AuthorInput> { new AuthorInput("Jane", "Smith") });
            var result = await _service.UpdateBookAsync(Guid.NewGuid(), updatedInput);
            Assert.False(result);
        }
    }
}