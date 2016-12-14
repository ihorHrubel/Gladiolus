using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApi.Providers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;

namespace WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
        IServiceCreator serviceCreator = new ServiceCreator();

        public void ConfigureAuth(IAppBuilder app)
        {           
            app.CreatePerOwinContext<IUserService>(CreateUserService);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };
                        
            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseFacebookAuthentication(
                appId: "1759786450949280",
                appSecret: "b381e4869883dfeda4009eda7c07ebce");

        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService();
        }
    }
}
