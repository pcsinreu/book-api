using book_api.Application.Book.Command;
using book_api.Application.Book.Commands;
using book_api.Application.Book.Query;
using book_api.Infraestructure;
using book_api.Interface.Input;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Dependecy Injection 
builder.Services.AddSingleton<IBookInfrastructure, BookInfrastructure>();
builder.Services.AddScoped<IBookCommandService, BookCommandService>();
builder.Services.AddScoped<IBookQueryService, BookQueryService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

builder.Services.AddValidatorsFromAssemblyContaining<BookInputValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateBookCommand>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5178")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}   

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
