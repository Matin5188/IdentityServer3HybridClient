# IdentityServer3HybridClient
Identity Server 3 with Hybrid MVC Client and API with Client Credential.
It uses In Memory Users, client and scopes for demo purpose.

# Identity Server
This server will be responsible for authentication of different clients
- https://localhost:44354/core
# MVC Application
This is MVC application which is one of the client and will be authenticated through identity server with hybrid flow.
Once User will Sign in into application, here it will only display claim of logged in user for demo purpose only.
- URL: https://localhost:44345
# Web API
This is web API which is one of the client and will be authenticated through identity server with client credential flow from within the MVC APP
After sign in in MVC App, you will have one of the button called "Call API" which will call API, before calling API it will fetch token from token endpoint of identity server and then once authenticated, it will call resource server or api endpoint, here api will only return claim of logged in user for demo purpose only.
- URL: https://localhost:44333/identity

# In Memory Users
- User Name: Matin
- Password: Matin@123

# References
- https://www.scottbrady91.com/Identity-Server/Identity-Server-3-Standalone-Implementation-Part-3
- https://leastprivilege.com/2014/10/10/openid-connect-hybrid-flow-and-identityserver-v3/
