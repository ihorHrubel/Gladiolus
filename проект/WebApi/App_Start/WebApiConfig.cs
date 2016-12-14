using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Cors;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            UnityConfig.RegisterComponents();

            EnableCorsAttribute CorsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(CorsAttribute);
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            
            config.MapHttpAttributeRoutes();            
        }
    }
}
