using book_api.Application.Book.Commands;
using book_api.Application.Book.Query;
using book_api.Interface.Input;
using book_api.Interface.Output;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using MediatR;

namespace book_api.Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator for handling requests.</param>
        /// <param name="logger">Service for logging.</param>
        public BookController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of books with optional sorting and filtering.
        /// </summary>
        /// <param name="sortBy">Sort by 'title', 'date', or 'authors'.</param>
        /// <param name="filter">Filter by title, author, or date.</param>
        /// <returns>List of books or NoContent if none found.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? sortBy, [FromQuery] string? filter)
        {
            var results = await _mediator.Send(new GetBooksQuery(sortBy, filter));
            if (results == null || results.Count == 0)
            {
                _logger.Log("No books found.");
                return NoContent();
            }
            _logger.Log($"{results.Count} books returned.");
            return Ok(results);
        }

        /// <summary>
        /// Registers a new book.
        /// </summary>
        /// <param name="bookInput">Book data.</param>
        /// <returns>Returns the ID of the created book.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookInput bookInput)
        {
            if (!ModelState.IsValid)
            {
                _logger.Log("Book creation failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            var id = await _mediator.Send(new CreateBookCommand(bookInput));
            _logger.Log($"Book created successfully. Id: {id}");
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        /// <summary>
        /// Updates an existing book by ID.
        /// </summary>
        /// <param name="id">Book ID.</param>
        /// <param name="bookInput">Updated book data.</param>
        /// <returns>NoContent if updated, NotFound if not found, BadRequest if invalid.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BookInput bookInput)
        {
            if (!ModelState.IsValid)
            {
                _logger.Log($"Book update failed for Id: {id} due to invalid model state.");
                return BadRequest(ModelState);
            }

            var updated = await _mediator.Send(new UpdateBookCommand(id, bookInput));
            if (!updated)
            {
                _logger.Log($"Book with Id: {id} not found for update.");
                return NotFound();
            }

            _logger.Log($"Book with Id: {id} updated successfully.");
            return NoContent();                                 
        }
    }
}