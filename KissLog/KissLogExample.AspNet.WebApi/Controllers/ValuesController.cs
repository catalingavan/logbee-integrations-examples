using KissLog;
using System.Collections;
using System.Web.Http;

namespace KissLogExample.AspNet.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IKLogger _logger;
        public ValuesController()
        {
            _logger = Logger.Factory.Get();
        }

        // GET api/values
        public IEnumerable Get()
        {
            _logger.Debug("Hello world from ASP.NET WebApi!");
            return new string[] { "value1", "value2" };
        }
    }
}
