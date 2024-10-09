# OpenTelemetry for .NET

.NET applications using [OpenTelemetry](https://github.com/open-telemetry/opentelemetry-dotnet) to send the logs and traces to logbee.net

[![OpenTelemetry.Exporter.OpenTelemetryProtocol](https://img.shields.io/nuget/v/OpenTelemetry.Exporter.OpenTelemetryProtocol.svg?style=flat-square&label=OpenTelemetry.Exporter.OpenTelemetryProtocol)](https://www.nuget.org/packages?q=OpenTelemetry.Exporter.OpenTelemetryProtocol)

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(tracingBuilder =>
    {
        tracingBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault()
               .AddAttributes(
               [
                   new("LogBee.OrganizationId", "_OrganizationId_"),
                   new("LogBee.ApplicationId", "_ApplicationId_")
               ])
           )
            .AddAspNetCoreInstrumentation()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("https://api.logbee.net/open-telemetry/v1/traces");
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
                new("LogBee.OrganizationId", "_OrganizationId_"),
                new("LogBee.ApplicationId", "_ApplicationId_")
            ]))
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("https://api.logbee.net/open-telemetry/v1/logs");
            opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
            opt.ExportProcessorType = ExportProcessorType.Batch;
        });
});
```