using KissLog;
using KissLog.AspNet.Web;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using System;
using System.Web.Http;

namespace KissLogExample.AspNet.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ConfigureKissLog();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                var logger = Logger.Factory.Get();
                logger.Error(exception);

                if (logger.AutoFlush() == false)
                {
                    Logger.NotifyListeners(logger);
                }
            }
        }

        private void ConfigureKissLog()
        {
            KissLogConfiguration.Listeners.Add(new RequestLogsApiListener(new Application(
                "0337cd29-a56e-42c1-a48a-e900f3116aa8",
                "4f729841-b103-460e-a87c-be6bd72f0cc9"
            ))
            {
                ApiUrl = "https://api.logbee.net/"
            });
        }

        public static KissLogHttpModule KissLogHttpModule = new KissLogHttpModule();

        public override void Init()
        {
            base.Init();

            KissLogHttpModule.Init(this);
        }
    }
}
