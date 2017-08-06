using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using ScottBrady91.IdentityServer3.Example.Configuration;

[assembly: OwinStartup(typeof(IdentityAuthServer.Startup))]

namespace IdentityAuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
            // configuration for setting up identity server 
            app.Map(
                "/core",
                coreApp =>
                {
                    coreApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Standalone Identity Server",
                        SigningCertificate = Cert.Load(),
                        Factory =
                        new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                        RequireSsl = true,
                        AuthenticationOptions = new AuthenticationOptions { EnablePostSignOutAutoRedirect = true }
                    });
                });
        }
    }
}
