# KissLog

.NET applications using [KissLog](https://github.com/catalingavan/KissLog.Sdk) to send logs to logbee.net

[![KissLog](https://img.shields.io/nuget/v/KissLog.svg?style=flat-square&label=KissLog)](https://www.nuget.org/packages?q=kisslog)
[![KissLog.AspNetCore](https://img.shields.io/nuget/v/KissLog.AspNetCore.svg?style=flat-square&label=KissLog.AspNetCore)](https://www.nuget.org/packages?q=kisslog)
[![KissLog.AspNet.Mvc](https://img.shields.io/nuget/v/KissLog.AspNet.Mvc.svg?style=flat-square&label=KissLog.AspNet.Mvc)](https://www.nuget.org/packages?q=kisslog)
[![KissLog.AspNet.WebApi](https://img.shields.io/nuget/v/KissLog.AspNet.WebApi.svg?style=flat-square&label=KissLog.AspNet.WebApi)](https://www.nuget.org/packages?q=kisslog)

[![Downloads](https://img.shields.io/nuget/dt/KissLog.svg?style=flat-square&label=Downloads)](https://www.nuget.org/packages?q=kisslog)

```csharp
using KissLog;
using KissLog.CloudListeners.RequestLogsListener;

KissLogConfiguration.Listeners
    .Add(new RequestLogsApiListener(new Application("_OrganizationId_", "_ApplicationId_"))
    {
        ApiUrl = "https://api.logbee.net/",
        UseAsync = false
    });

var logger = new Logger();

try
{
    logger.Info("Hello, KissLog");
}
catch (Exception ex)
{
    logger.Error(ex);
}
finally
{
    Logger.NotifyListeners(logger);
}
```
