namespace book_api.Infraestructure
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}