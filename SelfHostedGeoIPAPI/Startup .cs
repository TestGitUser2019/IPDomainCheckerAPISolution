using Owin;
using System.Web.Http;

namespace SelfHostedGeoIPAPI
{
   public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            appBuilder.UseWebApi(config);
        }
    }
}
