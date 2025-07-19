using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using book_api.Interface.Controllers;
using book_api.Interface.Input;
using book_api.Interface.Output;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using book_api.Application.Book.Query;
using book_api.Application.Book.Commands;

namespace book_api_testing.Controllers
{
    public class BookControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILoggerService> _loggerMock;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILoggerService>();
            _controller = new BookController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenBooksExist()
        {
            var books = new List<BookOutput>
        {
            new BookOutput(Guid.NewGuid(), "Title", DateTime.Now, new List<AuthorOutput>())
        };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBooksQuery>(), default)).ReturnsAsync(books);

            var result = await _controller.Get(null, null);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsNoContent_WhenNoBooks()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBooksQuery>(), default)).ReturnsAsync(new List<BookOutput>());

            var result = await _controller.Get(null, null);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreated_WhenModelIsValid()
        {
            var bookInput = new BookInput("Title", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), default)).ReturnsAsync(Guid.NewGuid());
            _controller.ModelState.Clear();

            var result = await _controller.Post(bookInput);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelIsInvalid()
        {
            var bookInput = new BookInput("", DateTime.Now, new List<AuthorInput>());
            _controller.ModelState.AddModelError("Title", "Required");

            var result = await _controller.Post(bookInput);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            var bookInput = new BookInput("Title", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), default)).ReturnsAsync(true);
            _controller.ModelState.Clear();

            var result = await _controller.Put(Guid.NewGuid(), bookInput);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenUpdateFails()
        {
            var bookInput = new BookInput("Title", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), default)).ReturnsAsync(false);
            _controller.ModelState.Clear();

            var result = await _controller.Put(Guid.NewGuid(), bookInput);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            var bookInput = new BookInput("", DateTime.Now, new List<AuthorInput>());
            _controller.ModelState.AddModelError("Title", "Required");

            var result = await _controller.Put(Guid.NewGuid(), bookInput);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}