using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace ScottBrady91.IdentityServer3.Example.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {                
                new Client
                {
                    ClientId = @"implicitclient",
                    ClientName = @"Implicit Web Form Client",
                    Enabled = true,
                    Flow = Flows.Implicit,
                    RequireConsent = true,
                    AllowRememberConsent = true,
                    RedirectUris = new List<string> {"https://localhost:44349/"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:44349/"},
                    AllowedScopes = 
                        new List<string>
                        {
                            Constants.StandardScopes.OpenId,
                            Constants.StandardScopes.Profile,
                            Constants.StandardScopes.Email
                        },
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientId = @"hybridclient",
                    ClientName = @"Hybrid Client", // will be display at consent screen.
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("idsrv3test".Sha256())
                    },
                    Enabled = true,
                    Flow = Flows.Hybrid, // HYBRID FLOW
                    RequireConsent = true,
                    AllowRememberConsent = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44345/" // Client URL, project name - IdentityServer.Client.MVC in this solution
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44345/"
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess,
                        "sampleApi"
                    },
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientName = "MVC Client API",
                    ClientId = "mvc_service",
                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "sampleApi"
                    }
                }
            };
        }
    }
}