using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityServer.Client.API.Startup))]

namespace IdentityServer.Client.API
{
    public class Startup
    {
        private const string IdServBaseUri = @"https://localhost:44354/core"; // Auth server url to for authentication
        public void Configuration(IAppBuilder app)
        {


            app.UseIdentityServerBearerTokenAuthentication(new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = IdServBaseUri,
                RequiredScopes = new[] { "sampleApi" }
            });
        }
    }
}
