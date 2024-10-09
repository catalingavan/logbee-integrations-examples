using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using KissLogExample.ConsoleApp.Example2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

ConfigureKissLog();

var services = new ServiceCollection();
services.AddLogging(logging =>
{
    logging
        .AddConfiguration(configuration.GetSection("Logging"))
        .AddKissLog(options =>
        {
            options.Formatter = (FormatterArgs args) =>
            {
                if (args.Exception == null)
                    return args.DefaultValue;

                string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
            };
        });
});

services.AddTransient<IMainService, MainService>();
var serviceProvider = services.BuildServiceProvider();

// set a global "Logger" that will be reused throughout the application execution
Logger.SetFactory(new KissLog.LoggerFactory(new Logger(url: "KissLogExample.ConsoleApp.Example3")));

ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Application started...");

try
{
    IMainService mainService = serviceProvider.GetRequiredService<IMainService>();
    await mainService.ExecuteAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "Unhandled exception");
}
finally
{
    var loggers = Logger.Factory.GetAll();
    Logger.NotifyListeners(loggers);
}

void ConfigureKissLog()
{
    KissLogConfiguration.Listeners
        .Add(new RequestLogsApiListener(new Application("0337cd29-a56e-42c1-a48a-e900f3116aa8", "4f729841-b103-460e-a87c-be6bd72f0cc9"))
        {
            ApiUrl = "https://api.logbee.net/",
            UseAsync = false
        });

    KissLogConfiguration.Options
        .AppendExceptionDetails((Exception ex) =>
        {
            StringBuilder sb = new StringBuilder();
            if (ex is NullReferenceException nullRefException)
            {
                sb.AppendLine("Important: check for null references");
            }

            return sb.ToString();
        });

    KissLogConfiguration.InternalLog = (msg) => Console.WriteLine(msg);
}
