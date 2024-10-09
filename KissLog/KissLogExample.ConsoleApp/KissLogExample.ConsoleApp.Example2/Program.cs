using KissLog;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using System.Text;

ConfigureKissLog();

// set a global "Logger" that will be reused throughout the application execution
Logger.SetFactory(new KissLog.LoggerFactory(new Logger(url: "KissLogExample.ConsoleApp.Example2")));

try
{
    Execute();
}
catch (Exception ex)
{
    var logger = Logger.Factory.Get();
    logger.Error(ex);
}
finally
{
    var loggers = Logger.Factory.GetAll();
    Logger.NotifyListeners(loggers);
}

void Execute()
{
    var logger = Logger.Factory.Get();
    logger.Info($"Started executing main code at {DateTime.UtcNow:o}");

    throw new NullReferenceException("Oops...");
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
