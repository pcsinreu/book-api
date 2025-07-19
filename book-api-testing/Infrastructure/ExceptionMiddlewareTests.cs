using Xunit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Moq;
using book_api.Infraestructure;

namespace book_api_testing.Infrastructure
{
    public class ExceptionMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_HandlesExceptionAndLogs()
        {
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILoggerService>();
            var middleware = new ExceptionMiddleware(_ => throw new Exception("Test"), new TestServiceProvider(loggerMock.Object));

            await middleware.InvokeAsync(context);

            Assert.Equal(500, context.Response.StatusCode);
            loggerMock.Verify(l => l.Log(It.Is<string>(msg => msg.Contains("Test"))), Times.Once);
        }

        private class TestServiceProvider : IServiceProvider
        {
            private readonly ILoggerService _logger;
            public TestServiceProvider(ILoggerService logger) => _logger = logger;
            public object GetService(Type serviceType) => _logger;
        }
    }
}