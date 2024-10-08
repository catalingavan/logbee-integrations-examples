﻿using Microsoft.Extensions.Logging;
using Serilog.Sinks.LogBee;
using System.Text.Json;

namespace Serilog.ConsoleApp.Example3
{
    internal class MainService : IMainService
    {
        private readonly LoggerContext _loggerContext;
        private readonly ILogger<MainService> _logger;
        public MainService(
            LoggerContext loggerContext,
            ILogger<MainService> logger)
        {
            _loggerContext = loggerContext ?? throw new ArgumentNullException(nameof(loggerContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task ExecuteAsync()
        {
            _logger.LogInformation("Executing main service at {DateTime}", DateTime.UtcNow);

            _loggerContext.LogAsFile(JsonSerializer.Serialize(new { Hello = "World" }), "File.json");

            throw new NullReferenceException("Oops...");
        }
    }
}
