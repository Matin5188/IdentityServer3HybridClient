using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Helpers;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using IdentityModel.Client;
using System.Security.Claims;
using System.Globalization;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Protocols;

[assembly: OwinStartup(typeof(IdentityServer.Client.MVC.Startup))]

namespace IdentityServer.Client.MVC
{
    public class Startup
    {
        private const string ClientUri = @"https://localhost:44345/"; // client/application url
        private const string IdServBaseUri = @"https://localhost:44354/core"; // Auth server url to for authentication
        private const string UserInfoEndpoint = IdServBaseUri + @"/connect/userinfo";
        private const string TokenEndpoint = IdServBaseUri + @"/connect/token"; // token endpoint

        public void Configuration(IAppBuilder app)
        {
            // Client Configuration
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = "Cookies" });

            // OPENID OWIN middleware
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "hybridclient",
                Authority = IdServBaseUri,
                RedirectUri = ClientUri,
                PostLogoutRedirectUri = ClientUri,
                ResponseType = "code id_token token", // indicates hybrid
                Scope = "openid profile email roles offline_access", // scopes
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                },
                SignInAsAuthenticationType = "Cookies",
                Notifications =
                        new OpenIdConnectAuthenticationNotifications
                        {
                            AuthorizationCodeReceived = async n =>
                            {
                                // Get the user claim
                                var userInfoClient = new UserInfoClient(new Uri(UserInfoEndpoint).ToString());
                                var userInfoResponse = await userInfoClient.GetAsync(n.ProtocolMessage.AccessToken);

                                var identity = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType);
                                identity.AddClaims(userInfoResponse.Claims);

                                // call to token endpoint by passing authorization code to get the access token and refresh token
                                var tokenClient = new TokenClient(TokenEndpoint, "hybridclient", "idsrv3test");
                                var response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);

                                // Add claim to authentication cookie.
                                identity.AddClaim(new Claim("access_token", response.AccessToken));
                                identity.AddClaim(
                                    new Claim("expires_at", DateTime.UtcNow.AddSeconds(response.ExpiresIn).ToLocalTime().ToString(CultureInfo.InvariantCulture)));
                                identity.AddClaim(new Claim("refresh_token", response.RefreshToken));
                                identity.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                                n.AuthenticationTicket = new AuthenticationTicket(
                                    identity,
                                    n.AuthenticationTicket.Properties);
                            },
                            RedirectToIdentityProvider = n =>
                            {
                                if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                                {
                                    var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                                    n.ProtocolMessage.IdTokenHint = idTokenHint;
                                }

                                return Task.FromResult(0);
                            }
                        }

            });
        }
    }
}
