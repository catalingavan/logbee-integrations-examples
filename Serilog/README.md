# Serilog

Example of .NET applications using [Serilog](https://github.com/serilog/serilog) to send the logs to logbee.net

```csharp
Log.Logger =
    new LoggerConfiguration()
        .WriteTo.LogBee(
            new LogBeeApiKey(
                "0337cd29-a56e-42c1-a48a-e900f3116aa8",
                "4f729841-b103-460e-a87c-be6bd72f0cc9",
                "https://api.logbee.net/"
            )
        )
        .CreateLogger();
```
