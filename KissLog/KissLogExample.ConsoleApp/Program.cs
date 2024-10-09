using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using KissLogExample.ConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

Logger.SetFactory(new KissLog.LoggerFactory(new Logger(url: "KissLogExample.ConsoleApp")));

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

ConfigureKissLog(configuration);

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

ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();

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

void ConfigureKissLog(IConfiguration configuration)
{
    KissLogConfiguration.Listeners
        .Add(new RequestLogsApiListener(new Application(configuration["LogBee.OrganizationId"], configuration["LogBee.ApplicationId"]))
        {
            ApiUrl = configuration["LogBee.ApiUrl"],
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



