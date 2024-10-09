using KissLog;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using System.Text;

ConfigureKissLog();

var logger = new Logger(url: "KissLogExample.ConsoleApp.Example1");

try
{
    logger.Info("Hello, KissLog");

    throw new NullReferenceException("Oops...");
}
catch (Exception ex)
{
    logger.Error(ex);
}
finally
{
    Logger.NotifyListeners(logger);
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
