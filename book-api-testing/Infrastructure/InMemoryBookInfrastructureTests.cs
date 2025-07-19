using Xunit;
using System;
using System.Collections.Generic;
using book_api.Interface.Input;

namespace book_api_testing.Infrastructure
{
    public class InMemoryBookInfrastructureTests
    {
        [Fact]
        public async Task SaveBookAsync_AddsBook()
        {
            var infra = new InMemoryBookInfrastructure();
            var input = new BookInput("Title", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            var id = await infra.SaveBookAsync(input);
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public async Task UpdateBookAsync_UpdatesBook_WhenExists()
        {
            var infra = new InMemoryBookInfrastructure();
            var input = new BookInput("Title", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            var id = await infra.SaveBookAsync(input);
            var updated = new BookInput("Updated", DateTime.Now, new List<AuthorInput> { new AuthorInput("Jane", "Smith") });
            var result = await infra.UpdateBookAsync(id, updated);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateBookAsync_ReturnsFalse_WhenNotExists()
        {
            var infra = new InMemoryBookInfrastructure();
            var updated = new BookInput("Updated", DateTime.Now, new List<AuthorInput> { new AuthorInput("Jane", "Smith") });
            var result = await infra.UpdateBookAsync(Guid.NewGuid(), updated);
            Assert.False(result);
        }
    }
}