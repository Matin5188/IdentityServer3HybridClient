using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace ScottBrady91.IdentityServer3.Example.Configuration
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "Matin",
                    Password = "Matin@123",
                    Subject = "1",
                    Claims = new List<Claim>
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Matin"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Momin"),
                        new Claim(Constants.ClaimTypes.Email, "matin_momin05@yahoo.co.in"),
                        new Claim(Constants.ClaimTypes.Role, "Admin")
                    }
                }
            };
        }
    }
}