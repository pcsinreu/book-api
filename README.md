# Book API

## Overview
Book API is a .NET 9 Web API for managing books and authors. It demonstrates Clean Architecture, SOLID, DRY, and YAGNI principles. The solution uses CQRS with MediatR, FluentValidation for input validation, and includes unit tests with xUnit and Moq.

## Features
- Book registration with title, publication date, and multiple authors.
- Book editing (title, date, authors).
- Book listing with sorting and filtering by title, date, and number of authors.
- Input validation using FluentValidation.
- CQRS pattern with MediatR for separation of commands and queries.
- Centralized exception handling middleware.
- Custom logger service for API actions.
- Interactive API documentation with Swagger.

## Project Structure
- `book-api/` - Main API project
  - `Application/` - Command and Query services, CQRS handlers
  - `Domain/` - Interfaces (e.g., ILoggerService)
  - `Infrastructure/` - Data access, logging, exception middleware
  - `Interface/` - Controllers, input/output models, validators
- `book-api-testing/` - xUnit test project
  - `Application/BookCommandServiceTests.cs` - Service unit tests
  - `Controllers/BookControllerTests.cs` - Controller unit tests
  - `Infrastructure/InMemoryBookInfrastructure.cs` - In-memory test infrastructure

## How to Run
1. Restore NuGet packages.
2. Build the solution.
3. Run the API project (`book-api`).
4. Access Swagger UI at `/swagger` for interactive documentation.
5. Run tests using your preferred test runner (xUnit).
