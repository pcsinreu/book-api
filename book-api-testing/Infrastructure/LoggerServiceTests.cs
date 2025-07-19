using book_api.Infraestructure;
using Xunit;

namespace book_api_testing.Infrastructure
{
    public class LoggerServiceTests
    {
        [Fact]
        public void Log_DoesNotThrow()
        {
            var logger = new LoggerService();
            var exception = Record.Exception(() => logger.Log("Test message"));
            Assert.Null(exception);
        }
    }
}