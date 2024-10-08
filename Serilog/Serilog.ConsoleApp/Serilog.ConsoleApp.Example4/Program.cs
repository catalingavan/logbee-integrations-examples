using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.ConsoleApp.Example4;
using Serilog.Sinks.LogBee;

var loggerContext = new NonWebLoggerContext();

Log.Logger =
    new LoggerConfiguration()
        .WriteTo.LogBee(
            new LogBeeApiKey(
                "0337cd29-a56e-42c1-a48a-e900f3116aa8",
                "4f729841-b103-460e-a87c-be6bd72f0cc9",
                "https://api.logbee.net/"
            ),
            loggerContext
        )
        .CreateLogger();

var services = new ServiceCollection();
services.AddLogging((builder) =>
{
    builder.AddSerilog();
});

// we register the loggerContext so we can inject it in different application services (such as MainService)
services.AddSingleton<LoggerContext>(loggerContext);

services.AddTransient<IMainService, MainService>();

var serviceProvider = services.BuildServiceProvider();

int i = 0;
while (true)
{
    using (IServiceScope scope = serviceProvider.CreateScope())
    {
        Console.WriteLine($"Execution number {i} begin");

        loggerContext.Reset($"http://application/Serilog.ConsoleApp.Example4/execution/{i}");

        var mainService = scope.ServiceProvider.GetRequiredService<IMainService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Execution running on: {time}", DateTimeOffset.Now);

        try
        {
            await mainService.ExecuteAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing main service");
        }
        finally
        {
            await loggerContext.FlushAsync();
        }
    }

    Console.WriteLine($"Execution number {i} completed");

    await Task.Delay(5000);
    i++;
}