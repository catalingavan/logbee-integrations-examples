using Microsoft.Extensions.Logging;

namespace KissLogExample.ConsoleApp.Example2
{
    internal class MainService : IMainService
    {
        private readonly ILogger<MainService> _logger;
        public MainService(
            ILogger<MainService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task ExecuteAsync()
        {
            _logger.LogInformation("Executing main service at {DateTime}", DateTime.UtcNow);

            throw new NullReferenceException("Oops...");
        }
    }
}
