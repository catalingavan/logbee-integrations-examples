using Serilog;
using Serilog.Sinks.LogBee;
using Serilog.Sinks.LogBee.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddSerilog((services, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.LogBee(
        new LogBeeApiKey(
            "0337cd29-a56e-42c1-a48a-e900f3116aa8",
            "4f729841-b103-460e-a87c-be6bd72f0cc9",
            "https://api.logbee.net/"
        ),
        services,
        (config) =>
        {
            config.ShouldReadRequestHeader = (request, header) =>
            {
                if (string.Equals(header.Key, "X-Api-Key", StringComparison.OrdinalIgnoreCase))
                    return false;

                return true;
            };
        }
    ));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseLogBeeMiddleware();

app.Run();
