using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddLogging(provider =>
{
    provider
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

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Hello from minimal API");

    return "Hello World!";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseKissLogMiddleware(options => {
    options.Listeners.Add(new RequestLogsApiListener(new Application(
        "0337cd29-a56e-42c1-a48a-e900f3116aa8",
        "4f729841-b103-460e-a87c-be6bd72f0cc9"
    ))
    {
        ApiUrl = "https://api.logbee.net/"
    });
});

app.Run();
