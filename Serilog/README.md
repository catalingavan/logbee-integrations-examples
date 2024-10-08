# Serilog

.NET applications using [Serilog.Sinks.LogBee](https://github.com/catalingavan/serilog-sinks-logbee) to send the Serilog events to Logbee.

[![Serilog.Sinks.LogBee](https://img.shields.io/nuget/v/Serilog.Sinks.LogBee.svg?style=flat-square&label=Serilog.Sinks.LogBee)](https://www.nuget.org/packages?q=Serilog.Sinks.LogBee)
[![Serilog.Sinks.LogBee.AspNetCore](https://img.shields.io/nuget/v/Serilog.Sinks.LogBee.AspNetCore.svg?style=flat-square&label=Serilog.Sinks.LogBee.AspNetCore)](https://www.nuget.org/packages?q=Serilog.Sinks.LogBee)

[![Downloads](https://img.shields.io/nuget/dt/Serilog.Sinks.LogBee.svg?style=flat-square&label=Downloads)](https://www.nuget.org/packages?q=Serilog.Sinks.LogBee)

```csharp
using Serilog;
using Serilog.Sinks.LogBee;

Log.Logger = new LoggerConfiguration()
    .WriteTo.LogBee(new LogBeeApiKey("_OrganizationId_", "_ApplicationId_", "https://api.logbee.net"))
    .CreateLogger();

try
{
    Log.Information("Hello from {Name}!", "Serilog");
}
catch(Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}
```
