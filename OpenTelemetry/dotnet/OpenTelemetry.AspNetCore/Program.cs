using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracingBuilder =>
    {
        tracingBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault()
               .AddAttributes(
               [
                   new("LogBee.OrganizationId", "0337cd29-a56e-42c1-a48a-e900f3116aa8"),
                   new("LogBee.ApplicationId", "4f729841-b103-460e-a87c-be6bd72f0cc9")
               ])
           )
            .AddAspNetCoreInstrumentation()
            .AddOtlpExporter(opt =>
            {
                // # send trace to logbee.net OpenTelemetry endpoint
                opt.Endpoint = new Uri("https://api.logbee.net/open-telemetry/v1/traces");

                // # send trace to OpenTelemetry Collector
                // opt.Endpoint = new Uri("http://localhost:4318/v1/traces");

                opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                opt.ExportProcessorType = ExportProcessorType.Batch;
            });
    });

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.IncludeFormattedMessage = true;

    options
        .SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddAttributes(
            [
                new("LogBee.OrganizationId", "0337cd29-a56e-42c1-a48a-e900f3116aa8"),
                new("LogBee.ApplicationId", "4f729841-b103-460e-a87c-be6bd72f0cc9")
            ]))
        .AddOtlpExporter(opt =>
        {
            // # send logs to logbee.net OpenTelemetry endpoint
            opt.Endpoint = new Uri("https://api.logbee.net/open-telemetry/v1/logs");

            // # send logs to OpenTelemetry Collector
            // opt.Endpoint = new Uri("http://localhost:4318/v1/logs");

            opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
            opt.ExportProcessorType = ExportProcessorType.Batch;
        });
});

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("My favourite cartoon is {Name}", "Futurama");
    logger.LogInformation("Today is {Today}", DateTime.Today.DayOfWeek);

    return "Hello World!";
});

app.Run();
