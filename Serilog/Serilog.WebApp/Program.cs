using Serilog;
using Serilog.Sinks.LogBee;
using Serilog.Sinks.LogBee.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddSerilog((services, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.LogBee(new LogBeeApiKey(
            builder.Configuration["LogBee.OrganizationId"]!,
            builder.Configuration["LogBee.ApplicationId"]!,
            builder.Configuration["LogBee.ApiUrl"]!
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
