using KissLog.AspNet.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace KissLogExample.AspNet.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionLogger), new KissLogExceptionLogger());
            config.Filters.Add(new KissLogWebApiExceptionFilterAttribute());

            config.MapHttpAttributeRoutes();

            // set the default path (/) to /api/values
            config.Routes.MapHttpRoute(
                name: "DefaultRoot",
                routeTemplate: "",
                defaults: new { controller = "Values" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
