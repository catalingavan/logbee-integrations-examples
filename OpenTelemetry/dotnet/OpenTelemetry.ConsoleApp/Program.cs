using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

ActivitySource source = new ActivitySource("ConsoleApp.OpenTelemetry", "1.0.0");

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry((opt) =>
    {
        opt.IncludeFormattedMessage = true;
        opt.IncludeScopes = true;

        opt.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddAttributes(
            [
                new("LogBee.OrganizationId", "0337cd29-a56e-42c1-a48a-e900f3116aa8"),
                new("LogBee.ApplicationId", "4f729841-b103-460e-a87c-be6bd72f0cc9")
            ]));

        opt
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
});

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .ConfigureResource(res =>
        res.AddAttributes(
        [
            new("LogBee.OrganizationId", "0337cd29-a56e-42c1-a48a-e900f3116aa8"),
            new("LogBee.ApplicationId", "4f729841-b103-460e-a87c-be6bd72f0cc9")
        ])
    )
    .AddSource(source.Name)
    .AddOtlpExporter(opt =>
    {
        // # send trace to logbee.net OpenTelemetry endpoint
        opt.Endpoint = new Uri("https://api.logbee.net/open-telemetry/v1/traces");

        // # send trace to OpenTelemetry Collector
        // opt.Endpoint = new Uri("http://localhost:4318/v1/traces");

        opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
        opt.ExportProcessorType = ExportProcessorType.Batch;
    })
    .Build();

var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Application has started...");

Foo(loggerFactory);

void Foo(ILoggerFactory loggerFactory)
{
    var logger = loggerFactory.CreateLogger<Program>();

    using (var parent = source.StartActivity("Foo", ActivityKind.Server))
    {
        parent?.AddTag("url.path", "/foo");

        logger.LogInformation("Executing Foo at {DateTime}", DateTime.UtcNow);
    }
}